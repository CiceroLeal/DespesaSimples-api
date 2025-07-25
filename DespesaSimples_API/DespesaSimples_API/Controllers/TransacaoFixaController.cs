using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Controllers;

public static class TransacaoFixaController
{
    public static async Task<IResult> GetTransacoesFixasAsync(
        ILogger<TransacaoFixaDto> logger,
        [FromServices] ITransacaoFixaService transacaoFixaService)
    {
        try
        {
            var response = await transacaoFixaService.BuscarTransacoesFixasAsync();

            return ApiResultsUtil.Success(response, "Transações fixas obtidas com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar transações fixas");
            return ApiResultsUtil.BadRequest("Erro ao buscar transações fixas");
        }
    }

    public static async Task<IResult> GetTransacaoFixaPorIdAsync(
        ILogger<TransacaoFixaDto> logger,
        [FromServices] ITransacaoFixaService transacaoFixaService,
        int transacaoFixaId)
    {
        try
        {
            var response = await transacaoFixaService.BuscarTransacaoFixaPorIdAsync(transacaoFixaId);

            return ApiResultsUtil.Success(response, "Transação fixa obtida com sucesso");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar transação fixa");
            return ApiResultsUtil.BadRequest("Erro ao buscar transação fixa");
        }
    }

    public static async Task<IResult> CriarTransacaoFixaAsync(
        ILogger<TransacaoFixaDto> logger,
        [FromServices] ITransacaoFixaService transacaoFixaService,
        [FromBody] TransacaoFixaFormDto transacaoFixaCriacaoDto)
    {
        try
        {
            var result = await transacaoFixaService.CriarTransacaoFixaAsync(transacaoFixaCriacaoDto);

            return result
                ? ApiResultsUtil.Success("Transação fixa criada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao criar transação fixa");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar transação fixa");
            return ApiResultsUtil.BadRequest("Erro ao criar transação fixa");
        }
    }
    
    public static async Task<IResult> CriarTransacoesParaMesAnoAsync(
        ILogger<TransacaoFixaDto> logger,
        [FromServices] ITransacaoFixaService transacaoFixaService,
        [FromQuery] int ano,
        [FromQuery] int mes)
    {
        try
        {
            var result = await transacaoFixaService.CriarTransacoesParaMesAnoAsync(ano, mes);

            return result
                ? ApiResultsUtil.Success("Transações criadas com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao criar transações");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar transações");
            return ApiResultsUtil.BadRequest("Erro ao criar transações");
        }
    }

    public static async Task<IResult> AtualizarTransacaoFixaAsync(
        ILogger<TransacaoFixaDto> logger,
        [FromServices] ITransacaoFixaService transacaoFixaService,
        int transacaoFixaId,
        [FromBody] TransacaoFixaFormDto transacaoFixaDto,
        [FromQuery(Name = "transacoes")] bool transacoesAnteriores = false)
    {
        try
        {
            var result = await transacaoFixaService.AtualizarTransacaoFixaAsync(transacaoFixaId, transacaoFixaDto,
                transacoesAnteriores);

            return result
                ? ApiResultsUtil.Success("Transação fixa atualizada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao atualizar transação fixa");
        }
        catch (NotFoundException)
        {
            return ApiResultsUtil.NotFound("Transação fixa não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar transação fixa");
            return ApiResultsUtil.BadRequest("Erro ao atualizar transação fixa");
        }
    }
    
    public static async Task<IResult> DeleteTransacaoFixaPorIdAsync(
        ILogger<TransacaoFixaDto> logger,
        [FromServices] ITransacaoFixaService transacaoFixaService,
        int transacaoFixaId,
        [FromQuery(Name = "transacoes")] bool transacoesAnteriores = false)
    {
        try
        {
            var result =
                await transacaoFixaService.RemoverTransacaoFixaPorIdAsync(transacaoFixaId, transacoesAnteriores);

            return result
                ? ApiResultsUtil.Success("Transação fixa apagada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao apagar transação fixa");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao apagar transação fixa");
            return ApiResultsUtil.BadRequest("Erro ao apagar transação fixa");
        }
    }
}