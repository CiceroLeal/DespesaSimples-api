using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Util;

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

    public static Transacao MapTransacaoDtoParaTransacao(TransacaoDto dto)
    {
        return new Transacao
        {
            Descricao = dto.Descricao ?? "",
            Valor = dto.Valor,
            Dia = dto.Dia,
            Mes = dto.Mes,
            Ano = dto.Ano,
            DataTransacao = dto.DataTransacao,
            Tipo = dto.Tipo ?? 0,
            Status = dto.Status ?? string.Empty,
            IdCategoria = dto.IdCategoria,
            IdCartao = dto.IdCartao
        };
    }
}