using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Services.Builders;

public class TransacaoFixaBuilder(
    ITransacaoFixaService transacaoFixaService,
    ITransacaoRepository transacaoRepository)
{
    public async Task<List<Transacao>> BuildAsync(
        List<Transacao> transacoes,
        int? ano,
        int? mes,
        TipoTransacaoEnum? tipo = null)
    {
        if (!ano.HasValue || !mes.HasValue)
            return [];

        var dataReferencia = new DateTime(ano.Value, mes.Value, 1);
        if (dataReferencia <= DateTime.Now)
            return [];

        var fixas = await transacaoFixaService
            .BuscarTransacoesFixasPorMesAnoAsync(mes.Value, ano.Value, tipo);

        if (fixas.Count == 0)
            return [];

        var idsTransacoesFixasJaProcessadas = await ObterIdsDeTransacoesFixasJaProcessadas(transacoes, ano.Value, mes.Value, tipo);
        
        //Remove transações que já existem naquele mes, inclusive as deletadas
        //Só adiciona as transações que não tem transacoes atreladas
        fixas.RemoveAll(t => idsTransacoesFixasJaProcessadas.Contains(t.IdTransacaoFixa ?? 0));

        return MapFixasDtoParaEntidadeTransacao(fixas, mes.Value, ano.Value);
    }

    private async Task<HashSet<int>> ObterIdsDeTransacoesFixasJaProcessadas(List<Transacao> transacoes, int ano, int mes, TipoTransacaoEnum? tipo)
    {
        var ids = transacoes
            .Select(t => t.IdTransacaoFixa)
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();
        
        var idsSet = new HashSet<int>(ids);

        var transacoesDeletadas = await transacaoRepository
            .BuscarTransacoesDeletadasPorMesAnoAsync(ano, mes, tipo);

        foreach (var tDeletada in transacoesDeletadas.Where(t => t.IdTransacaoFixa.HasValue))
        {
            idsSet.Add(tDeletada.IdTransacaoFixa!.Value);
        }

        return idsSet;
    }

    private static List<Transacao> MapFixasDtoParaEntidadeTransacao(IEnumerable<TransacaoFixaDto> fixasDto, int mes, int ano)
    {
        return fixasDto.Select(fixa => new Transacao
        {
            Descricao = fixa.Descricao,
            Valor = fixa.Valor,
            Tipo = fixa.Tipo,
            IdCategoria = fixa.IdCategoria,
            Categoria = fixa.Categoria != null ? CategoriaMapper.MapDtoParaCategoria(fixa.Categoria) : null,
            IdCartao = fixa.IdCartao,
            Cartao = fixa.Cartao != null ? CartaoMapper.MapDtoParaCartao(fixa.Cartao) : null,
            IdTransacaoFixa = fixa.IdTransacaoFixa,
            Mes = mes,
            Ano = ano,
            Dia = fixa.DataInicio.Day,
            Status = StatusCalculadorUtil.CalculaStatus(fixa.DataInicio.Day, mes, ano)
        }).ToList();
    }
} 