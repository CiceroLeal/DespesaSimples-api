using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Mappers;

public static class TransacaoMapper
{
    public static TransacaoDto MapParaDto(Transacao transacao)
    {
        return new TransacaoDto
        {
            IdTransacao = transacao.IdTransacao == 0
                ? $"{transacao.IdTransacaoFixa ?? 0}F"
                : transacao.IdTransacao.ToString(),
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Dia = transacao.Dia ?? 0,
            Ano = transacao.Ano,
            Mes = transacao.Mes,
            DataTransacao = transacao.DataTransacao,
            Parcela = transacao.Parcela,
            Status = transacao.Status,
            IdCategoria = transacao.IdCategoria,
            IdCartao = transacao.IdCartao,
            Tags = (transacao.Tags ?? new List<Tag>()).Select(tag => tag.Nome).ToList(),
            Tipo = transacao.Tipo,
            SubTransacoes = []
        };
    }
}