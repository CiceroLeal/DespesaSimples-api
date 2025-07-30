using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Util;

public static class TransacaoUtil
{
    public static decimal ValorEfetivoTransacao(TransacaoDto transacaoDto)
    {
        if (transacaoDto.Tipo == null)
            return transacaoDto.Valor;
        
        return transacaoDto.Tipo switch
        {
            TipoTransacaoEnum.Receita => transacaoDto.Valor,
            TipoTransacaoEnum.Despesa => -transacaoDto.Valor,
            _ => 0
        };
    }
    
    public static void CalcularValoresHierarquicos(List<TransacaoDto> nos)
    {
        foreach (var no in nos)
        {
            CalcularValoresHierarquicos(no.SubTransacoes);
            
            if (no.Tipo == null)
            {
                no.Valor = no.SubTransacoes.Sum(ValorEfetivoTransacao);
            }
        }
    }
    
    public static List<TransacaoDto> FiltrarETotalizarPorTags(
        List<TransacaoDto> nos,
        List<string> tags,
        bool matchAny = false)
    {
        var resultado = new List<TransacaoDto>();

        foreach (var no in nos)
        {
            var filhosFiltrados = FiltrarETotalizarPorTags(no.SubTransacoes, tags, matchAny);
            
            var isTransacaoReal = no.Tipo != null;
            var containsTags = false;
            
            if (isTransacaoReal && no.Tags?.Count > 0)
            {
                containsTags = matchAny
                    ? no.Tags.Intersect(tags).Any()
                    : tags.All(t => no.Tags.Contains(t));
            }

            if (!containsTags && filhosFiltrados.Count == 0) 
                continue;
            
            no.SubTransacoes = filhosFiltrados;
                
            if (no.Tipo == null)
                no.Valor = no.SubTransacoes.Sum(ValorEfetivoTransacao);
                
            resultado.Add(no);
        }

        return resultado;
    }
}