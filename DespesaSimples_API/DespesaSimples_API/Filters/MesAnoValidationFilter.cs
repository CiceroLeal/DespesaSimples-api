using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Filters;

public class MesAnoValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var errors = new Dictionary<string, string[]>();
        
        int? mes = null;
        int? ano = null;

        foreach (var arg in context.Arguments)
        {
            if (arg is not int i) 
                continue;
            
            if (ano == null) ano = i;
            else if (mes == null) mes = i;
        }
        
        if (mes is < 1 or > 12)
            errors[nameof(mes)] = ["Mês deve estar entre 1 e 12."];
        
        if(ano is < 1900)
            errors[nameof(ano)] = ["Ano deve ser maior que 1900."];

        if (errors.Count != 0)
            return Util.ApiResultsUtil.BadRequest(errors);

        return await next(context);
    }
}