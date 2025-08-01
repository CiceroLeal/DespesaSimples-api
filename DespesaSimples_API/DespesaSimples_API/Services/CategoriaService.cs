using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Commands;
using DespesaSimples_API.Commands.Transacao;
using DespesaSimples_API.Dtos.Categoria;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Util;
using MediatR;

namespace DespesaSimples_API.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository, IMediator mediator)
    : ICategoriaService
{
    public async Task<List<CategoriaDto>> BuscarCategoriasAsync()
    {
        var categorias = await categoriaRepository.BuscarCategoriasAsync();

        return categorias.Select(CategoriaMapper.MapCategoriaParaDto).ToList();
    }

    public async Task<CategoriaDto?> BuscarCategoriaDtoPorIdAsync(string id)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Categoria);
        var categoria = await categoriaRepository.BuscarCategoriaPorIdAsync(idInt ?? 0);

        return categoria != null ? CategoriaMapper.MapCategoriaParaDto(categoria) : null;
    }

    public async Task<List<CategoriaDto>> BuscarCategoriaEPaisPorIdAsync(string id)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Categoria);
        var categorias = await categoriaRepository.BuscarCategoriaEPaisAsync(idInt ?? 0);

        return categorias.Select(CategoriaMapper.MapCategoriaParaDto).ToList();
    }

    public async Task<bool> CriarCategoriaAsync(CategoriaFormDto categoriaFormDto)
    {
        ArgumentNullException.ThrowIfNull(categoriaFormDto);

        var categoria = CategoriaMapper.MapCategoriaFormDtoParaCategoria(categoriaFormDto);

        return await categoriaRepository.CriarCategoriaAsync(categoria);
    }

    public async Task<bool> AtualizarCategoriaAsync(string id, CategoriaFormDto categoriaFormDto)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Categoria);

        var categoria = await categoriaRepository.BuscarCategoriaPorIdAsync(idInt ?? 0);

        if (categoria == null)
            throw new NotFoundException();

        var diaAnterior = categoria.Dia;

        categoria.Nome = categoriaFormDto.Nome;
        categoria.Descricao = categoriaFormDto.Descricao;
        categoria.Dia = categoriaFormDto.Dia;
        categoria.IdCategoriaPai =
            IdUtil.ParseIdToInt(categoriaFormDto.IdCategoriaPai, (char)TipoCategoriaEnum.Categoria);

        var result = await categoriaRepository.AtualizarCategoriaAsync(categoria);

        if (!result || categoriaFormDto.Dia == null || diaAnterior == categoriaFormDto.Dia)
            return result;

        // Dia da Categoria foi alterado, atualizar transações futuras caso existam
        var dataAtual = DateTime.Now;

        await mediator.Send(new AtualizarDiaTransacoesFuturasCommand(TipoCategoriaEnum.Categoria,
            idInt ?? 0,
            (int)categoriaFormDto.Dia,
            dataAtual.Year,
            dataAtual.Month));

        return result;
    }

    public async Task<bool> RemoverCategoriaPorIdAsync(string id)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Categoria) ?? 0;
        return await categoriaRepository.RemoverCategoriaAsync(idInt);
    }
}