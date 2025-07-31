using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Filters;

public class MesAnoValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var errors = new Dictionary<string, string[]>();
        var request = context.HttpContext.Request;

        var anoStr = request.RouteValues["ano"]?.ToString() ?? request.Query["ano"].FirstOrDefault();
        var mesStr = request.RouteValues["mes"]?.ToString() ?? request.Query["mes"].FirstOrDefault();


        if (anoStr != null)
        {
            if (int.TryParse(anoStr, out var ano))
            {
                if (ano < 1900)
                    errors["ano"] = ["Ano deve ser maior que 1900."];
            }
            else
            {
                errors["ano"] = ["Ano deve ser um número inteiro válido."];
            }
        }

        if (mesStr != null)
        {
            if (int.TryParse(mesStr, out var mes))
            {
                if (mes is < 1 or > 12)
                    errors["mes"] = ["Mês deve estar entre 1 e 12."];
            }
            else
            {
                errors["mes"] = ["Mês deve ser um número inteiro válido."];
            }
        }

        if (errors.Count > 0)
            return ApiResultsUtil.BadRequest(errors);

        return await next(context);
    }
}