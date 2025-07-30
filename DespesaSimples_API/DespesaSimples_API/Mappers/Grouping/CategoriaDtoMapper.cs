using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Mappers.Grouping;

public static class CategoriaDtoMapper
{
    public static TransacaoDto MapParaTransacaoDto(Categoria categoria, List<Transacao> transacoesFilhas)
    {
        var subTransacoesDto = transacoesFilhas.Select(TransacaoMapper.MapParaDto).ToList();

        return new TransacaoDto
        {
            IdTransacao = $"{categoria.IdCategoria}C",
            Descricao = categoria.Nome,
            Valor = subTransacoesDto.Sum(TransacaoUtil.ValorEfetivoTransacao),
            Status = StatusCalculadorUtil.CalculaStatusGrupo(transacoesFilhas),
            Dia = categoria.Dia,
            Mes = subTransacoesDto.FirstOrDefault()?.Mes ?? DateTime.Now.Month,
            Ano = subTransacoesDto.FirstOrDefault()?.Ano ?? DateTime.Now.Year,
            IdCategoria = categoria.IdCategoriaPai,
            SubTransacoes = subTransacoesDto,
            Tags = subTransacoesDto
                .SelectMany(t => t.Tags ?? [])
                .Distinct()
                .ToList()
        };
    }
}