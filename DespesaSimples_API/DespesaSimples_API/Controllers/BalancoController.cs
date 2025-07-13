using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Balanco;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Controllers;

public static class BalancoController
{
    public static async Task<IResult> GetBalancoMesAnoAsync(
        ILogger<BalancoDto> logger,
        [FromServices] IBalancoService balancoService,
        [FromQuery] int mes,
        [FromQuery] int ano)
    {
        try
        {
            var response = await balancoService.ObterPorAnoMesAsync(ano, mes);
            return ApiResultsUtil.Success(response, "Balanços obtidos com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter balanços");
            return ApiResultsUtil.BadRequest("Erro ao buscar balanços");
        }
    }

    public static async Task<IResult> GetBalancoAnoAsync(
        ILogger<BalancoDto> logger,
        [FromServices] IBalancoService balancoService,
        [FromRoute] int ano)
    {
        try
        {
            var response = await balancoService.ObterPorAnoAsync(ano);
            return ApiResultsUtil.Success(response, "Balanços obtidos com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter balanços");
            return ApiResultsUtil.BadRequest("Erro ao buscar balanços");
        }
    }
}