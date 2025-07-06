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

        if (result is IStatusCodeHttpResult { StatusCode: (int)HttpStatusCode.NotFound })
        {
            logger.LogInformation("Resource {RequestPath} was not found.", context.HttpContext.Request.Path);
            return new NotFoundObjectResult(new ApiResponse<object>
            {
                Success = false,
                Errors = [new ApiError { Code = "NOT_FOUND", Message = "Recurso não encontrado." }],
                Message = "O recurso solicitado não foi encontrado."
            });
        }

        return result;
    }
}