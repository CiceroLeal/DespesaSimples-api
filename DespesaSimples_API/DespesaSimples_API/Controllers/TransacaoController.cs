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
            var transacoes = await transacaoService.BuscarTransacoesAsync(ano, mes, tipo, tags.ToList());

            var response = new TransacaoResponseDto
            {
                Transacoes = transacoes
            };

            return ApiResultsUtil.Success(response, "Transações obtidas com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar transações");
            return ApiResultsUtil.BadRequest("Erro ao buscar transações");
        }
    }

    public static async Task<IResult> GetTransacaoPorIdAsync(
        ILogger<TransacaoDto> logger,
        ITransacaoService transacaoService,
        int transacaoId)
    {
        try
        {
            var transacao = await transacaoService.BuscarTransacaoPorIdAsync(transacaoId);

            var response = new TransacaoResponseDto
            {
                Transacoes = transacao != null ? [transacao] : []
            };

            return ApiResultsUtil.Success(response, "Transação obtida com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar transação");
            return ApiResultsUtil.BadRequest("Erro ao buscar transação");
        }
    }

    public static async Task<IResult> GetTransacaoPorIdTransacaoFixaAsync(
        ILogger<TransacaoDto> logger,
        ITransacaoService transacaoService,
        string transacaoFuturaId,
        [FromQuery] int mes,
        [FromQuery] int ano)
    {
        try
        {
            var transacao = await transacaoService.BuscarTransacaoPorIdTransacaoFixaAsync(transacaoFuturaId, mes, ano);

            var response = new TransacaoResponseDto
            {
                Transacoes = transacao != null ? [transacao] : []
            };

            return ApiResultsUtil.Success(response, "Transação obtida com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar transação");
            return ApiResultsUtil.BadRequest("Erro ao buscar transação");
        }
    }
}