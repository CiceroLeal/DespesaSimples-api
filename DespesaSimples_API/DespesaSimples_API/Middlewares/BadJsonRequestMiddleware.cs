using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Middlewares;

public class BadJsonRequestMiddleware(RequestDelegate next, ILogger<BadJsonRequestMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (BadHttpRequestException ex)
            when (ex.Message.StartsWith("Failed to read parameter"))
        {
            logger.LogWarning(ex, "JSON inválido na requisição em {Path}", httpContext.Request.Path);

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            httpContext.Response.ContentType = "application/problem+json";

            var result = Util.ApiResultsUtil.BadRequest(new Dictionary<string, string[]>
            {
                { "JsonParsing", ["Erro ao ler Json, verifique se os campos estão corretos e tente novamente"] }
            });
            
            await result.ExecuteAsync(httpContext);
        }
    }
}