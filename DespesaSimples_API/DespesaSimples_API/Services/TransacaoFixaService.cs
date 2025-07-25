using DespesaSimples_API.Abstractions.Infra;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Commands;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Queries;
using DespesaSimples_API.Util;
using MediatR;

namespace DespesaSimples_API.Services;

public class TransacaoFixaService(
    ITransacaoFixaRepository transacaoFixaRepository,
    ITransactionManager transactionManager,
    IMediator mediator
)
    : ITransacaoFixaService
{
    public async Task<TransacaoFixaResponseDto> BuscarTransacoesFixasAsync()
    {
        var transacoesFixas = await transacaoFixaRepository.BuscarTodasTransacoesFixasAsync();

        return new TransacaoFixaResponseDto
        {
            Transacoes = transacoesFixas.Select(TransacaoFixaMapper.MapParaDto).ToList()
        };
    }

    public async Task<TransacaoFixaResponseDto> BuscarTransacaoFixaPorIdAsync(int id)
    {
        var transacaoFixa = await transacaoFixaRepository.BuscarTransacaoFixaPorIdAsync(id);

        return new TransacaoFixaResponseDto
        {
            Transacoes = transacaoFixa != null ? [TransacaoFixaMapper.MapParaDto(transacaoFixa)] : []
        };
    }

    public async Task<bool> CriarTransacaoFixaAsync(TransacaoFixaFormDto dto)
    {
        await transactionManager.BeginTransactionAsync();
        try
        {
            ArgumentNullException.ThrowIfNull(dto);

            var tags = await mediator.Send(
                new BuscarAtualizarTagsCommand(dto.Tags ?? [])
            );

            var transacaoFixa = TransacaoFixaMapper.MapTransacaoFixaFormDtoParaTransacaoFixa(dto, tags);

            await transacaoFixaRepository.CriarTransacaoFixaAsync(transacaoFixa);

            var tDto = TransacaoFixaMapper.MapTransacaoFixaParaTransacaoDto(transacaoFixa, dto.Finalizada ?? false);

            var result = await mediator.Send(new CriarTransacoesAPartirDaFixaCommand(tDto, dto.DataTermino, tags));

            await transactionManager.CommitAsync();

            return result;
        }
        catch
        {
            await transactionManager.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> CriarTransacoesParaMesAnoAsync(int ano, int mes)
    {
        await transactionManager.BeginTransactionAsync();

        try
        {
            var dataReferencia = new DateTime(ano, mes, 1);
            var transacoesFixas = await transacaoFixaRepository.BuscarTodasTransacoesFixasAsync();

            foreach (var transacaoFixa in transacoesFixas
                         .Where(transacaoFixa =>
                             transacaoFixa.DataInicio <= dataReferencia &&
                             (!transacaoFixa.DataTermino.HasValue || transacaoFixa.DataTermino.Value >= dataReferencia)
                         )
                    )
            {
                var transacaoExistente = await mediator.Send(
                    new BuscarTransacaoPorIdFixaMesAnoQuery(transacaoFixa.IdTransacaoFixa, mes, ano));

                if (transacaoExistente != null)
                    continue;

                var transacaoFixaDto =
                    TransacaoFixaMapper.MapTransacaoFixaParaTransacaoDto(transacaoFixa);

                if (!await mediator.Send(new CriarTransacaoAPartirDaFixaCommand(transacaoFixaDto)))
                    throw new Exception("Erro ao criar transação fixa para o mês e ano especificados.");
            }

            await transactionManager.CommitAsync();
            return true;
        }
        catch
        {
            await transactionManager.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> AtualizarTransacaoFixaAsync(
        int id,
        TransacaoFixaFormDto dto,
        bool transacaoAnteriores)
    {
        await transactionManager.BeginTransactionAsync();
        try
        {
            var transacaoFixa = await transacaoFixaRepository.BuscarTransacaoFixaPorIdAsync(id);

            if (transacaoFixa == null)
                throw new NotFoundException();

            var tags = await mediator.Send(
                new BuscarAtualizarTagsCommand(dto.Tags ?? [])
            );

            var dataInicioAntiga = transacaoFixa.DataInicio;
            var dataTerminoAntiga = transacaoFixa.DataTermino;

            var dataInicioAlterada = transacaoFixa.DataInicio.Day != dto.DataInicio.Day ||
                                     transacaoFixa.DataInicio.Month != dto.DataInicio.Month ||
                                     transacaoFixa.DataInicio.Year != dto.DataInicio.Year;

            var dataTerminoAlterada = transacaoFixa.DataTermino?.Day != dto.DataTermino?.Day ||
                                      transacaoFixa.DataTermino?.Month != dto.DataTermino?.Month ||
                                      transacaoFixa.DataTermino?.Year != dto.DataTermino?.Year;

            transacaoFixa.Valor = dto.Valor;
            transacaoFixa.Descricao = dto.Descricao;
            transacaoFixa.DataInicio = dto.DataInicio;
            transacaoFixa.DataTermino = dto.DataTermino;
            transacaoFixa.IdCategoria = IdUtil.ParseIdToInt(dto.Categoria, (char)TipoCategoriaEnum.Categoria);
            transacaoFixa.IdCartao = IdUtil.ParseIdToInt(dto.Cartao, (char)TipoCategoriaEnum.Cartao);
            transacaoFixa.Tags = tags;

            var result = await transacaoFixaRepository.AtualizarTransacaoFixaAsync(transacaoFixa);

            var transacaoDto =
                TransacaoFixaMapper.MapTransacaoFixaParaTransacaoDto(transacaoFixa, dto.Finalizada ?? false);

            await LidaComTransacoesAnterioresAsync(
                transacaoFixa,
                transacaoDto,
                dataInicioAntiga,
                dataTerminoAntiga,
                transacaoAnteriores,
                dataInicioAlterada,
                dataTerminoAlterada);

            await transactionManager.CommitAsync();
            return result;
        }
        catch
        {
            await transactionManager.RollbackAsync();
            throw;
        }
    }

    private async Task LidaComTransacoesAnterioresAsync(
        TransacaoFixa transacaoFixa,
        TransacaoDto transacaoDto,
        DateTime dataAntiga,
        DateTime? dataTerminoAntiga,
        bool transacaoAnteriores,
        bool dataInicioAlterada,
        bool dataTerminoAlterada)
    {
        if (!transacaoAnteriores)
            return;

        if (dataInicioAlterada || dataTerminoAlterada)
        {
            var dataNovaInicio = new DateTime(transacaoFixa.DataInicio.Year, transacaoFixa.DataInicio.Month, 1);
            var dataAntigaInicio = new DateTime(dataAntiga.Year, dataAntiga.Month, 1);
            var dataTermino = DataUtil.NormalizarParaInicioDoMes(transacaoFixa.DataTermino ?? DateTime.Now);
            var dataFinal = dataTermino > DateTime.Now ? DataUtil.NormalizarParaInicioDoMes(DateTime.Now) : dataTermino;

            if (dataNovaInicio <= dataAntigaInicio)
                await IncluirOuAtualizarTransacoesAsync(
                    transacaoFixa, transacaoDto, dataNovaInicio, dataFinal
                );
            else // dataNovaInicio > dataAntigaInicio
                await ExcluirTransacoesAsync(transacaoFixa, dataAntigaInicio, dataNovaInicio);

            if (dataTerminoAlterada)
                await LidaComDataTerminoAsync(transacaoFixa, transacaoDto, dataTerminoAntiga);
        }
        else
        {
            await mediator.Send(new AtualizarTransacoesAPartirDaFixaCommand(
                transacaoFixa.IdTransacaoFixa,
                transacaoDto,
                transacaoFixa.Tags?.ToList() ?? []));
        }

        await mediator.Send(
            new ReprocessarBalancosCommand(
                new DateTime(transacaoFixa.DataInicio.Year, transacaoFixa.DataInicio.Month,
                    transacaoFixa.DataInicio.Day)
            )
        );
    }

    private async Task LidaComDataTerminoAsync(TransacaoFixa transacaoFixa, TransacaoDto transacaoFixaDto,
        DateTime? dataTerminoAntiga)
    {
        var dataAtualReferencia = DataUtil.ObterDataReferencia();
        var dataTermino = transacaoFixa.DataTermino;

        if (!dataTermino.HasValue)
        {
            await TratarDataTerminoVaziaAsync(transacaoFixa, transacaoFixaDto, dataAtualReferencia);
            return;
        }

        var dataTerminoNovaNorm = DataUtil.NormalizarParaInicioDoMes(dataTermino.Value);

        if (!dataTerminoAntiga.HasValue)
        {
            await TratarDataTerminoAnteriorIndefinidaAsync(transacaoFixa, dataTerminoNovaNorm,
                dataAtualReferencia);
            return;
        }

        var dataTerminoAntigaNorm = DataUtil.NormalizarParaInicioDoMes(dataTerminoAntiga.Value);

        if (dataTerminoNovaNorm < dataTerminoAntigaNorm)
        {
            await TratarDataTerminoRetroativaAsync(transacaoFixa, dataTerminoNovaNorm);
        }
        else if (dataTerminoNovaNorm > dataTerminoAntigaNorm)
        {
            await TratarDataTerminoProgressivaAsync(transacaoFixa, transacaoFixaDto, dataTerminoAntigaNorm,
                dataTerminoNovaNorm, dataAtualReferencia);
        }
    }

    // Se a nova DataTermino estiver vazia, inclui transações até o mês atual a partir da última transação cadastrada.
    private async Task TratarDataTerminoVaziaAsync(TransacaoFixa transacaoFixa, TransacaoDto transacaoDto,
        DateTime dataAtualReferencia)
    {
        var ultimaTransacao =
            await mediator.Send(new BuscarUltimaTransacaoPorIdFixaQuery(transacaoFixa.IdTransacaoFixa));

        var inicioInclusao = ultimaTransacao != null
            ? new DateTime(ultimaTransacao.Ano, ultimaTransacao.Mes, 1)
            : DataUtil.ObterDataReferencia();

        await IncluirOuAtualizarTransacoesAsync(
            transacaoFixa,
            transacaoDto,
            inicioInclusao,
            dataAtualReferencia);
    }

    // Se a DataTermino antiga era indefinida e agora foi definida, inclui transações começando da data atual.
    private async Task TratarDataTerminoAnteriorIndefinidaAsync(
        TransacaoFixa transacaoFixa, 
        DateTime dataTerminoNovaNorm, 
        DateTime dataAtualReferencia)
    {
        if (dataTerminoNovaNorm < dataAtualReferencia)
        {
            await ExcluirTransacoesAsync(
                transacaoFixa, 
                dataTerminoNovaNorm.AddMonths(1), 
                dataAtualReferencia.AddMonths(1));
        }
    }

    // Se a nova DataTermino foi alterada para uma data anterior, exclui transações posteriores à nova data.
    private async Task TratarDataTerminoRetroativaAsync(TransacaoFixa transacaoFixa, DateTime dataTerminoNovaNorm)
    {
        var inicioExclusao = dataTerminoNovaNorm.AddMonths(1);
        var ultimaTransacao =
            await mediator.Send(new BuscarUltimaTransacaoPorIdFixaQuery(transacaoFixa.IdTransacaoFixa));

        if (ultimaTransacao != null)
        {
            var fimExclusao = new DateTime(ultimaTransacao.Ano, ultimaTransacao.Mes, 1).AddMonths(1);
            await ExcluirTransacoesAsync(transacaoFixa, inicioExclusao, fimExclusao);
        }
    }

    // Se a nova DataTermino foi alterada para uma data posterior, inclui transações até o limite informado (ou até o mês atual).
    private async Task TratarDataTerminoProgressivaAsync(TransacaoFixa transacaoFixa, TransacaoDto transacaoDto,
        DateTime dataTerminoAntigaNorm, DateTime dataTerminoNovaNorm, DateTime dataAtualReferencia)
    {
        var inicioInclusao = dataTerminoAntigaNorm.AddMonths(1);
        var fimInclusao = dataTerminoNovaNorm <= dataAtualReferencia
            ? dataTerminoNovaNorm
            : dataAtualReferencia;

        await IncluirOuAtualizarTransacoesAsync(
            transacaoFixa,
            transacaoDto,
            inicioInclusao,
            fimInclusao);
    }

    private async Task IncluirOuAtualizarTransacoesAsync(
        TransacaoFixa transacaoFixa,
        TransacaoDto transacaoDto,
        DateTime dataInicial,
        DateTime? dataFinal = null)
    {
        dataFinal ??= DateTime.Now;

        for (var iterator = dataInicial; iterator <= dataFinal; iterator = iterator.AddMonths(1))
        {
            var transacao = await mediator.Send(
                new BuscarTransacaoPorIdFixaMesAnoQuery(
                    transacaoFixa.IdTransacaoFixa, iterator.Month, iterator.Year
                )
            );

            var dto = transacaoDto.Clone();

            dto.Ano = iterator.Year;
            dto.Mes = iterator.Month;
            dto.Dia = transacaoFixa.DataInicio.Day;

            await mediator.Send(transacao != null
                ? new AtualizarTransacaoAPartirDaFixaCommand(
                    int.Parse(transacao.IdTransacao),
                    dto,
                    transacaoFixa.Tags?.ToList() ?? []
                )
                : new CriarTransacaoAPartirDaFixaCommand(dto)
            );
        }
    }

    private async Task ExcluirTransacoesAsync(TransacaoFixa transacaoFixa, DateTime dataInicial, DateTime dataFinal)
    {
        for (var iterator = dataInicial; iterator < dataFinal; iterator = iterator.AddMonths(1))
        {
            var transacao = await mediator.Send(
                new BuscarTransacaoPorIdFixaMesAnoQuery(
                    transacaoFixa.IdTransacaoFixa, iterator.Month, iterator.Year
                )
            );

            if (transacao != null)
                await mediator.Send(new RemoverTransacaoCommand(int.Parse(transacao.IdTransacao)));
        }
    }

    public async Task<bool> RemoverTransacaoFixaPorIdAsync(int id, bool transacoesAnteriores)
    {
        await transactionManager.BeginTransactionAsync();
        try
        {
            if (transacoesAnteriores)
                await mediator.Send(new RemoverTransacoesPorIdTransacaoFixaCommand(id));

            var result = await transacaoFixaRepository.RemoverTransacaoFixaAsync(id);
            await transactionManager.CommitAsync();

            return result;
        }
        catch
        {
            await transactionManager.RollbackAsync();
            throw;
        }
    }
}