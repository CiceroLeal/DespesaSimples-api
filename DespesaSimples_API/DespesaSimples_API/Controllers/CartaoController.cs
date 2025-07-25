using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Cartao;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Controllers;

public static class CartaoController
{
    public static async Task<IResult> GetCartoesAsync(
        ILogger<CartaoDto> logger,
        [FromServices] ICartaoService cartaoService,
        [FromQuery] int? mes,
        [FromQuery] int? ano)
    {
        try
        {
            var response = await cartaoService.BuscarCartoesAsync(mes, ano);
            
            return ApiResultsUtil.Success(response, "Cartões obtidos com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar cartões");
            return ApiResultsUtil.BadRequest("Erro ao buscar cartões");
        }
    }

    public static async Task<IResult> GetCartaoPorIdAsync(
        ILogger<CartaoDto> logger,
        [FromServices] ICartaoService cartaoService,
        string cartaoId)
    {
        try
        {
            var response = await cartaoService.BuscarCartaoPorIdAsync(cartaoId);
            
            return ApiResultsUtil.Success(response, "Cartão obtido com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar cartão");
            return ApiResultsUtil.BadRequest("Erro ao buscar cartão");
        }
    }

    public static async Task<IResult> DeleteCartaoPorIdAsync(
        ILogger<CartaoDto> logger,
        [FromServices] ICartaoService cartaoService,
        string cartaoId)
    {
        try
        {
            var result = await cartaoService.RemoverCartaoPorIdAsync(cartaoId);

            return result
                ? ApiResultsUtil.Success("Cartão removido com sucesso")
                : ApiResultsUtil.NotFound("Cartão não encontrado");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao remover cartão");
            return ApiResultsUtil.BadRequest("Erro ao remover cartão");
        }
    }

    public static async Task<IResult> CriarCartaoAsync(
        ILogger<CartaoDto> logger,
        [FromServices] ICartaoService cartaoService,
        [FromBody] CartaoFormDto cartaoCriacaoDto)
    {
        try
        {
            var result = await cartaoService.CriarCartaoAsync(cartaoCriacaoDto);

            return result
                ? ApiResultsUtil.Success("Cartão criado com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao criar cartão");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar cartão");
            return ApiResultsUtil.BadRequest("Erro ao criar cartão");
        }
    }

    public static async Task<IResult> AtualizarCartaoAsync(
        ILogger<CartaoDto> logger,
        [FromServices] ICartaoService cartaoService,
        string cartaoId,
        [FromBody] CartaoFormDto cartaoAtualizacaoDto)
    {
        try
        {
            var result = await cartaoService.AtualizarCartaoAsync(cartaoId, cartaoAtualizacaoDto);

            return result
                ? ApiResultsUtil.Success("Cartão atualizado com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao atualizar cartão");
        }
        catch (NotFoundException)
        {
            return ApiResultsUtil.NotFound("Cartão não encontrado");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar cartão");
            return ApiResultsUtil.BadRequest("Erro ao atualizar cartão");
        }
    }
}