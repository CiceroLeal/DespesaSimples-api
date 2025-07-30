using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Mappers.Grouping;

public static class CartaoDtoMapper
{
    public static TransacaoDto MapParaTransacaoDto(Cartao cartao, List<Transacao> transacoesFilhas)
    {
        var subTransacoesDto = transacoesFilhas.Select(TransacaoMapper.MapParaDto).ToList();

        return new TransacaoDto
        {
            IdTransacao = $"{cartao.IdCartao}D",
            Descricao = cartao.Nome,
            Valor = subTransacoesDto.Sum(TransacaoUtil.ValorEfetivoTransacao),
            Status = StatusCalculadorUtil.CalculaStatusGrupo(transacoesFilhas),
            Dia = cartao.DiaVencimento,
            Mes = subTransacoesDto.FirstOrDefault()?.Mes ?? DateTime.Now.Month,
            Ano = subTransacoesDto.FirstOrDefault()?.Ano ?? DateTime.Now.Year,
            IdCategoria = cartao.IdCategoria,
            IdCartao = cartao.IdCartao,
            SubTransacoes = subTransacoesDto,
            Tags = subTransacoesDto
                .SelectMany(t => t.Tags ?? [])
                .Distinct()
                .ToList()
        };
    }
}