using DespesaSimples_API.Dtos.Cartao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Mappers;

public static class CartaoMapper
{
    public static CartaoDto MapCartaoParaDto(Cartao cartao)
    {
        return new CartaoDto
        {
            IdCartao = cartao.IdCartao + "D",
            Limite = cartao.Limite,
            Nome = cartao.Nome,
            Bandeira = cartao.Bandeira,
            Descricao = cartao.Descricao,
            DiaFechamento = cartao.DiaFechamento,
            DiaVencimento = cartao.DiaVencimento,
            ValorParcial = cartao.TotalMes,
            IdCategoria = cartao.IdCategoria != null ? cartao.IdCategoria + "C" : null,
            Transacoes = cartao.Transacoes.Select(TransacaoMapper.MapParaDto).ToList(),
            Categoria = cartao.Categoria != null ? 
                CategoriaMapper.MapCategoriaParaDto(cartao.Categoria) : 
                null
        };
    }
    
    public static Cartao MapDtoParaCartao(CartaoDto cartaoDto)
    {
        return new Cartao
        {
            IdCartao = IdUtil.ParseIdToInt(cartaoDto.IdCartao, (char)TipoCategoriaEnum.Cartao),
            Limite = cartaoDto.Limite,
            Nome = cartaoDto.Nome,
            Bandeira = cartaoDto.Bandeira,
            Descricao = cartaoDto.Descricao,
            DiaFechamento = cartaoDto.DiaFechamento,
            DiaVencimento = cartaoDto.DiaVencimento,
            TotalMes = cartaoDto.ValorParcial ?? 0,
            IdCategoria = cartaoDto.IdCategoria != null 
                ? IdUtil.ParseIdToInt(cartaoDto.IdCategoria, (char)TipoCategoriaEnum.Categoria)
                : null
        };
    }

    public static Cartao MapCartaoCriacaoDtoParaCartao(CartaoFormDto cartaoCriacaoDto)
    {
        return new Cartao
        {
            Nome = cartaoCriacaoDto.Nome,
            Limite = cartaoCriacaoDto.Limite,
            Bandeira = cartaoCriacaoDto.Bandeira,
            Descricao = cartaoCriacaoDto.Descricao,
            DiaFechamento = cartaoCriacaoDto.DiaFechamento,
            DiaVencimento = cartaoCriacaoDto.DiaVencimento,
            IdCategoria = IdUtil.ParseIdToInt(cartaoCriacaoDto.IdCategoria, (char)TipoCategoriaEnum.Categoria)
        };
    }
}