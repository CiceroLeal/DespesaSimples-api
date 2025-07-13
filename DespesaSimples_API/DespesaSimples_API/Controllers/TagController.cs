using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Tag;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Controllers;

public static class TagController
{
    public static async Task<IResult> GetTagsAsync(
        ILogger<TagDto> logger,
        [FromServices] ITagService tagService)
    {
        try
        {
            var response = await tagService.ObterTodasTagsAsync();
            return ApiResultsUtil.Success(response, "Tags obtidas com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter tags");
            return ApiResultsUtil.BadRequest("Erro ao buscar tags");
        }
    }

    public static async Task<IResult> GetTagPorIdAsync(
        ILogger<TagDto> logger,
        [FromServices] ITagService tagService,
        int tagId)
    {
        try
        {
            var tag = await tagService.ObterTagPorIdAsync(tagId);
            return ApiResultsUtil.Success(tag);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter tag");
            return ApiResultsUtil.BadRequest("Erro ao buscar tag");
        }
    }

    public static async Task<IResult> DeleteTagPorIdAsync(
        ILogger<TagDto> logger,
        [FromServices] ITagService tagService,
        int tagId)
    {
        try
        {
            var result = await tagService.RemoverTagPorIdAsync(tagId);

            return result
                ? ApiResultsUtil.Success("Tag removida com sucesso")
                : ApiResultsUtil.NotFound("Tag não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao remover tag");
            return ApiResultsUtil.BadRequest("Erro ao remover tag");
        }
    }

    public static async Task<IResult> CriarTagAsync(
        ILogger<TagDto> logger,
        [FromServices] ITagService tagService,
        [FromBody] TagDto tagDto)
    {
        try
        {
            var result = await tagService.CriarTagAsync(tagDto);

            return result
                ? ApiResultsUtil.Success("Tag criada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao criar tag");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar tag");
            return ApiResultsUtil.BadRequest("Erro ao criar tag");
        }
    }

    public static async Task<IResult> AtualizarTagAsync(
        ILogger<TagDto> logger,
        [FromServices] ITagService tagService,
        int tagId,
        [FromBody] TagDto tagDto)
    {
        try
        {
            var result = await tagService.AtualizarTagAsync(tagId, tagDto);

            return result
                ? ApiResultsUtil.Success("Tag atualizada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao atualizar tag");
        }
        catch (NotFoundException)
        {
            return ApiResultsUtil.NotFound("Tag não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar tag");
            return ApiResultsUtil.BadRequest("Erro ao atualizar tag");
        }
    }
}