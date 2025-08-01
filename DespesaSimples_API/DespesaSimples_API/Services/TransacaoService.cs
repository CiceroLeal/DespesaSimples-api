using DespesaSimples_API.Abstractions.Infra;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Abstractions.Services.Factories;
using DespesaSimples_API.Commands;
using DespesaSimples_API.Commands.TransacaoFixa;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Queries;
using DespesaSimples_API.Services.Builders;
using DespesaSimples_API.Util;
using MediatR;

namespace DespesaSimples_API.Services;

public class TransacaoService(
    ITransacaoRepository transacaoRepository,
    TransacaoFixaBuilder transacaoFixaBuilder,
    ITransacaoFactory transacaoFactory,
    ITransactionManager transactionManager,
    IMediator mediator) : ITransacaoService
{
    public async Task<List<TransacaoDto>> BuscarTransacoesAsync(int? ano, int? mes, TipoTransacaoEnum? tipo,
        List<string> tags)
    {
        var transacoesVariaveis = await transacaoRepository
            .BuscarTransacoesPorMesAnoAsync(ano, mes, tipo);

        var transacoesFixas = await transacaoFixaBuilder.BuildAsync(transacoesVariaveis, ano, mes, tipo);
        var todasTransacoes = transacoesVariaveis.Concat(transacoesFixas).ToList();
        var todasDto = new TransacaoDtoBuilder(todasTransacoes).Build();

        return tags.Count == 0
            ? todasDto
            : TransacaoUtil.FiltrarETotalizarPorTags(todasDto, tags);
    }

    public async Task<TransacaoDto?> BuscarTransacaoPorIdAsync(int id)
    {
        var transacao = await transacaoRepository.BuscarTransacaoPorIdAsync(id);

        return transacao != null ? TransacaoMapper.MapParaDto(transacao) : null;
    }

    public async Task<TransacaoDto?> BuscarTransacaoPorIdTransacaoFixaAsync(string id, int mes, int ano)
    {
        var transacaoId = IdUtil.ParseIdToInt(id, 'F');

        if (transacaoId == null)
            return null;

        var transacaoFixa = await mediator.Send(new BuscarTransacaoFixaPorIdQuery((int)transacaoId));

        if (transacaoFixa == null)
            return null;

        transacaoFixa.DataInicio = new DateTime(ano, mes, transacaoFixa.DataInicio.Day);

        return TransacaoFixaMapper.MapFixaDtoParaDto(transacaoFixa);
    }

    public async Task<TransacaoDto?> BuscarTransacaoPorIdFixaMesAnoAsync(int idTransacaoFixa, int mes, int ano)
    {
        var transacao = await transacaoRepository.BuscarTransacaoPorIdFixaMesAnoAsync(idTransacaoFixa, mes, ano);
        return transacao != null ? TransacaoMapper.MapParaDto(transacao) : null;
    }

    public async Task<TransacaoDto?> BuscarUltimaTransacaoPorIdFixaAsync(int idTransacaoFixa)
    {
        var transacao = await transacaoRepository.BuscarUltimaTransacaoPorIdFixaAsync(idTransacaoFixa);
        return transacao != null ? TransacaoMapper.MapParaDto(transacao) : null;
    }

    public async Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes)
    {
        return await transacaoRepository
            .SomarPorTipoAsync(
                tipo,
                ano,
                mes);
    }
    
    public async Task<bool> CriarTransacaoAsync(TransacaoCriacaoDto transacaoCriacaoDto)
    {
        if (transacaoCriacaoDto == null)
            throw new NotFoundException();

        await transactionManager.BeginTransactionAsync();
        try
        {
            var tags = await mediator.Send(new BuscarAtualizarTagsCommand(transacaoCriacaoDto.Tags ?? []));
            
            if (transacaoCriacaoDto.Fixa)
                await CriarTransacaoFixaAsync(transacaoCriacaoDto, tags);
            else
            {
                var transacoesParaSalvar = transacaoFactory.Create(transacaoCriacaoDto, tags);
                await transacaoRepository.CriarTransacaoAsync(transacoesParaSalvar);
            }
            
            await mediator.Send(
                new ReprocessarBalancosCommand(transacaoCriacaoDto.DataVencimento)
            );

            await transactionManager.CommitAsync();
            return true;
        }
        catch
        {
            await transactionManager.RollbackAsync();
            throw;
        }
    }
    
    private async Task CriarTransacaoFixaAsync(TransacaoCriacaoDto dto, List<Tag> tags)
    {
        var transacaoFixaDto = await mediator.Send(new CriarTransacaoFixaAPartirDoCriacaoDtoCommand(dto, tags));

        if (transacaoFixaDto == null)
            throw new ApplicationException("Erro ao criar transação fixa");

        var transacaoDto = TransacaoFixaMapper.MapFixaDtoParaDto(transacaoFixaDto);
        
        await CriarTransacoesAPartirDaFixaAsync(transacaoDto, dto.DataTransacao, tags);
    }

    public async Task<bool> CriarTransacaoAPartirDaFixaAsync(TransacaoDto dto)
    {
        var transacao = TransacaoMapper.MapTransacaoDtoParaTransacao(dto);
        var tags = await mediator.Send(new BuscarAtualizarTagsCommand(dto.Tags ?? []));

        transacao.IdTransacaoFixa = int.Parse(
            dto.IdTransacao.EndsWith('F') ? dto.IdTransacao[..^1] : dto.IdTransacao
        );

        transacao.Tags = tags;

        return await transacaoRepository.CriarTransacaoAsync(transacao);
    }

    public async Task<bool> CriarTransacoesAPartirDaFixaAsync(TransacaoDto dto, DateTime? dataTermino, List<Tag> tags)
    {
        var dataAtualizacao = new DateTime(dto.Ano, dto.Mes, dto.Dia ?? 1);
        var dataFim = dataTermino ?? DateTime.Now;

        while (dataAtualizacao.Year < dataFim.Year ||
               (dataAtualizacao.Year == dataFim.Year && dataAtualizacao.Month <= dataFim.Month))
        {
            var transacao = TransacaoMapper.MapTransacaoDtoParaTransacao(dto);

            transacao.IdTransacaoFixa = int.Parse(
                dto.IdTransacao.EndsWith('F') ? dto.IdTransacao[..^1] : dto.IdTransacao
            );

            transacao.Dia = dataAtualizacao.Day;
            transacao.Mes = dataAtualizacao.Month;
            transacao.Ano = dataAtualizacao.Year;
            transacao.Tags = tags;

            await transacaoRepository.CriarTransacaoAsync(transacao);

            dataAtualizacao = dataAtualizacao.AddMonths(1);
        }

        return true;
    }

    public async Task<bool> AtualizarTransacaoAPartirDaFixaAsync(int id, TransacaoDto dto, List<Tag> tags)
    {
        var transacao = await transacaoRepository.BuscarTransacaoPorIdAsync(id);

        if (transacao == null)
            return false;

        transacao.Descricao = dto.Descricao;
        transacao.Valor = dto.Valor;
        transacao.Dia = dto.Dia;
        transacao.Mes = dto.Mes;
        transacao.Ano = dto.Ano;
        transacao.DataTransacao = dto.DataTransacao;
        transacao.IdCategoria = dto.IdCategoria;
        transacao.IdCartao = dto.IdCartao;
        transacao.Tags = tags;

        var sucesso = await transacaoRepository.AtualizarTransacaoAsync(transacao);

        if (sucesso)
            await mediator.Send(
                new ReprocessarBalancosCommand(new DateTime(dto.Ano, dto.Mes, dto.Dia ?? 1))
            );

        return sucesso;
    }

    public async Task<bool> AtualizarDiaTransacoesFuturasAsync(
        TipoCategoriaEnum tipo,
        int idCategoria,
        int novoDia,
        int anoAtual,
        int mesAtual)
    {
        if (tipo == TipoCategoriaEnum.Categoria)
            return await transacaoRepository
                .AtualizarDiaTransacoesFuturasPorCategoriaAsync(idCategoria, novoDia, anoAtual, mesAtual);

        return await transacaoRepository
            .AtualizarDiaTransacoesFuturasPorCartaoAsync(idCategoria, novoDia, anoAtual, mesAtual);
    }

    public async Task<bool> AtualizarTransacoesAPartirDaFixaAsync(int idTransacaoFixa, TransacaoDto dto, List<Tag> tags)
    {
        try
        {
            var transacoes = await transacaoRepository.BuscarTransacaoPorIdFixaAsync(idTransacaoFixa);

            foreach (var transacao in transacoes)
            {
                transacao.Valor = dto.Valor;
                transacao.Descricao = dto.Descricao;
                transacao.IdCategoria = dto.IdCategoria;
                transacao.IdCartao = dto.IdCartao;
                transacao.Tags?.Clear();
                transacao.Tags = tags;

                await transacaoRepository.AtualizarTransacaoAsync(transacao);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoverTransacaoPorIdAsync(int id)
    {
        return await transacaoRepository.RemoverTransacaoAsync(id);
    }

    public async Task<bool> RemoverTransacoesPorIdTransacaoFixaAsync(int idTransacaoFixa)
    {
        return await transacaoRepository.RemoverTransacoesPorIdTransacaoFixaAsync(idTransacaoFixa);
    }
}