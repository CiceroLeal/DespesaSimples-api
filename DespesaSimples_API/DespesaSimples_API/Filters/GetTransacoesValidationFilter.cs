using DespesaSimples_API.Enums;
using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Filters;

public class GetTransacoesValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var errors = new Dictionary<string, string[]>();
        
        var http = context.HttpContext;
        var q = http.Request.Query;

        
        var mes = q.TryGetValue("mes", out var mesVals) && int.TryParse(mesVals.FirstOrDefault(), out var m) 
            ? m 
            : DateTime.Now.Month;

        var ano = q.TryGetValue("ano", out var anoVals) && int.TryParse(anoVals.FirstOrDefault(), out var a)
            ? a
            : DateTime.Now.Year;

        TipoTransacaoEnum? tipoEnum = null;
        if (q.TryGetValue("tipo", out var tipoVals) && !string.IsNullOrWhiteSpace(tipoVals.FirstOrDefault()))
        {
            var s = tipoVals.First();
            if (Enum.TryParse<TipoTransacaoEnum>(s, true, out var parsed))
                tipoEnum = parsed;
            else
            {
                errors["tipo"] = ["Tipo de transação inválido"];
            }
        }

        // Injecta nos argumentos: [0]=ILogger, [1]=ITransacaoService, [2]=mes, [3]=ano, [4]=tipo
        context.Arguments[2] = mes;
        context.Arguments[3] = ano;
        context.Arguments[4] = tipoEnum;

        if (errors.Count != 0)
            return Util.ApiResultsUtil.BadRequest(errors);

        return await next(context);
    }
}