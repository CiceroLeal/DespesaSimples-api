using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Exceptions;
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
    
    public static async Task<IResult> CriarTransacaoAsync(
        ILogger<TransacaoDto> logger,
        ITransacaoService transacaoService,
        TransacaoCriacaoDto transacaoCriacaoDto)
    {
        try
        {
            var result = await transacaoService.CriarTransacaoAsync(transacaoCriacaoDto);

            return result
                ? ApiResultsUtil.Success("Transação criada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao criar transação");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar transação");
            return ApiResultsUtil.BadRequest("Erro ao criar transação");
        }
    }
    
    public static async Task<IResult> AtualizarTransacaoAsync(
        ILogger<TransacaoDto> logger,
        ITransacaoService transacaoService,
        int transacaoId,
        [FromQuery(Name = "parcelas")]bool? editarParcelas,
        TransacaoAtualizacaoDto transacaoAtualizacaoDto)
    {
        try
        {
            var result = await transacaoService
                .AtualizarTransacaoAsync(transacaoId, transacaoAtualizacaoDto, editarParcelas ?? false);

            return result
                ? ApiResultsUtil.Success("Transação atualizada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao atualizar transação");
        }
        catch (NotFoundException)
        {
            return ApiResultsUtil.NotFound("Transação não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar transação");
            return ApiResultsUtil.BadRequest("Erro ao atualizar transação");
        }
    }
    
    public static async Task<IResult> AtualizarTransacaoFuturaAsync(
        ILogger<TransacaoDto> logger,
        ITransacaoService transacaoService,
        string transacaoFuturaId,
        TransacaoFuturaAtualizacaoDto transacaoAtualizacaoDto)
    {
        try
        {
            var result = await transacaoService
                .AtualizarTransacaoFixaFuturaAsync(transacaoFuturaId, transacaoAtualizacaoDto);

            return result
                ? ApiResultsUtil.Success("Transação atualizada com sucesso")
                : ApiResultsUtil.BadRequest("Erro ao atualizar transação");
        }
        catch (NotFoundException)
        {
            return ApiResultsUtil.NotFound("Transação não encontrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar transação");
            return ApiResultsUtil.BadRequest("Erro ao atualizar transação");
        }
    }
}