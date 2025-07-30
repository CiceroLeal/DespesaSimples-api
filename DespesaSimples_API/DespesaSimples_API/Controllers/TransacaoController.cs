using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Controllers;

public static class TransacaoController
{
    public static async Task<IResult> GetTransacoesAsync(
        ILogger<TransacaoDto> logger,
        ITransacaoService transacaoService,
        [FromQuery] int mes,
        [FromQuery] int ano,
        [FromQuery] TipoTransacaoEnum? tipo,
        [FromQuery] string[] tags)
    {
        try
        {
            var response = await transacaoService.BuscarTransacoesAsync(ano, mes, tipo, tags.ToList());

            return ApiResultsUtil.Success(response, "Transações fixas obtidas com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar transações fixas");
            return ApiResultsUtil.BadRequest("Erro ao buscar transações fixas");
        }
    }
}