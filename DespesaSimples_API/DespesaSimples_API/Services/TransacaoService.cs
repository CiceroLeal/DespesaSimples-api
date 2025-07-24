using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Commands;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Util;
using MediatR;

namespace DespesaSimples_API.Services;

public class TransacaoService(ITransacaoRepository transacaoRepository, IMediator mediator) : ITransacaoService
{
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