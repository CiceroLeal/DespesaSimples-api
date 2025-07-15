using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Categoria;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Controllers;

public static class CategoriaController
{
    public static async Task<IResult> GetCategoriasAsync(
        ILogger<CategoriaDto> logger,
        [FromServices] ICategoriaService categoriaService)
    {
        try
        {
            var response = await categoriaService.ObterCategoriasAsync();
            return ApiResultsUtil.Success(response, "Categorias obtidas com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter categorias");
            return ApiResultsUtil.BadRequest("Erro ao buscar categorias");
        }
    }

    public static async Task<IResult> GetCategoriaPorIdAsync(
        ILogger<CategoriaDto> logger,
        [FromServices] ICategoriaService categoriaService,
        string categoriaId)
    {
        try
        {
            var response = await categoriaService.ObterCategoriaDtoPorIdAsync(categoriaId);
            return ApiResultsUtil.Success(response, "Categoria obtida com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter categoria");
            return ApiResultsUtil.BadRequest("Erro ao buscar categoria");
        }
    }

    public static async Task<IResult> DeleteCategoriaPorIdAsync(
        ILogger<CategoriaDto> logger,
        [FromServices] ICategoriaService categoriaService,
        string categoriaId)
    {
        try
        {
            var result = await categoriaService.RemoverCategoriaPorIdAsync(categoriaId);

            return result
                ? ApiResultsUtil.Success("Categoria removida com sucesso")
                : ApiResultsUtil.NotFound("Categoria não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao remover categoria");
            return ApiResultsUtil.BadRequest("Erro ao remover categoria");
        }
    }

    public static async Task<IResult> CriarCategoriaAsync(
        ILogger<CategoriaDto> logger,
        [FromServices] ICategoriaService categoriaService,
        [FromBody] CategoriaFormDto categoriaFormDto)
    {
        try
        {
            var result = await categoriaService.CriarCategoriaAsync(categoriaFormDto);

            return result
                ? ApiResultsUtil.Success("Categoria criada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao criar categoria");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar categoria");
            return ApiResultsUtil.BadRequest("Erro ao criar categoria");
        }
    }

    public static async Task<IResult> AtualizarCategoriaAsync(
        ILogger<CategoriaDto> logger,
        [FromServices] ICategoriaService categoriaService,
        string categoriaId,
        [FromBody] CategoriaFormDto categoriaFormDto)
    {
        try
        {
            var result = await categoriaService.AtualizarCategoriaAsync(categoriaId, categoriaFormDto);

            return result
                ? ApiResultsUtil.Success("Categoria atualizada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao atualizar categoria");
        }
        catch (NotFoundException)
        {
            return ApiResultsUtil.NotFound("Categoria não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar categoria");
            return ApiResultsUtil.BadRequest("Erro ao atualizar categoria");
        }
    }
}