using DespesaSimples_API.Dtos.Categoria;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Mappers;

public static class CategoriaMapper
{
    public static CategoriaDto MapCategoriaParaDto(Categoria categoria)
    {
        return new CategoriaDto
        {
            IdCategoria = categoria.IdCategoria + "C",
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            IdCategoriaPai = categoria.IdCategoriaPai != null ? categoria.IdCategoriaPai + "C" : null,
            NomeCategoriaPai = categoria.IdCategoriaPai != null ? categoria.CategoriaPai?.Nome : null,
            Dia = categoria.Dia,
            Transacoes = categoria.Transacoes.Select(TransacaoMapper.MapParaDto).ToList()
        };
    }
    
    public static Categoria MapDtoParaCategoria(CategoriaDto categoriaDto)
    {
        return new Categoria
        {
            IdCategoria = IdUtil.ParseIdToInt(categoriaDto.IdCategoria, (char)TipoCategoriaEnum.Categoria),
            IdCategoriaPai = IdUtil.ParseIdToInt(categoriaDto.IdCategoriaPai, (char)TipoCategoriaEnum.Categoria),
            Nome = categoriaDto.Nome,
            Descricao = categoriaDto.Descricao,
            Dia = categoriaDto.Dia
        };
    }

    public static Categoria MapCategoriaFormDtoParaCategoria(CategoriaFormDto categoriaFormDto)
    {
        return new Categoria
        {
            Nome = categoriaFormDto.Nome,
            Descricao = categoriaFormDto.Descricao,
            IdCategoriaPai = IdUtil.ParseIdToInt(categoriaFormDto.IdCategoriaPai, (char)TipoCategoriaEnum.Categoria),
            Dia = categoriaFormDto.Dia
        };
    }
}