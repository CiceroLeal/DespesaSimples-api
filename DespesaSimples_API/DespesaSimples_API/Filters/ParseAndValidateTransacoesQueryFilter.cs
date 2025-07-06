using DespesaSimples_API.Enums;
using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Filters;

public class ParseAndValidateTransacoesQueryFilter(ILogger<ParseAndValidateTransacoesQueryFilter> logger)
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var http = context.HttpContext;
        var q = http.Request.Query;

        int mes;
        if (q.TryGetValue("mes", out var mesVals) && int.TryParse(mesVals.FirstOrDefault(), out var m))
        {
            mes = m;
        }
        else
        {
            mes = DateTime.Now.Month;
            logger.LogDebug("Query 'mes' não informada ou inválida. Usando mês atual: {Mes}", mes);
        }

        int ano;
        if (q.TryGetValue("ano", out var anoVals) && int.TryParse(anoVals.FirstOrDefault(), out var a))
        {
            ano = a;
        }
        else
        {
            ano = DateTime.Now.Year;
            logger.LogDebug("Query 'ano' não informada ou inválida. Usando ano atual: {Ano}", ano);
        }

        TipoTransacaoEnum? tipoEnum = null;
        if (q.TryGetValue("tipo", out var tipoVals) && !string.IsNullOrWhiteSpace(tipoVals.FirstOrDefault()))
        {
            var s = tipoVals.First();
            if (!Enum.TryParse<TipoTransacaoEnum>(s, true, out var parsed))
            {
                logger.LogWarning("Query 'tipo' inválida: {Tipo}", s);
                return new BadRequestObjectResult(new ApiResponse<object>
                {
                    Success = false,
                    Errors = [new ApiError { Code = "INVALID_QUERY", Message = "Tipo de transação inválido." }],
                    Message = "Erro de validação nos parâmetros da query."
                });
            }
        }

        // Injecta nos argumentos: [0]=ILogger, [1]=ITransacaoService, [2]=mes, [3]=ano, [4]=tipo
        context.Arguments[2] = mes;
        context.Arguments[3] = ano;
        context.Arguments[4] = tipoEnum;

        return await next(context);
    }
}