using System.Net;
using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Filters;

public class LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is not IStatusCodeHttpResult { StatusCode: (int)HttpStatusCode.NotFound }) 
            return result;
        
        logger.LogInformation("Recurso {RequestPath} não encontrado", context.HttpContext.Request.Path);
        return Util.ApiResultsUtil.NotFound();
    }
}