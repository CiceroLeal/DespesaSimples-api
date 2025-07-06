using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DespesaSimples_API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var response = new ApiResponse<object>
        {
            Success = false,
            Errors = [new ApiError { Code = "EXCEPTION", Message = context.Exception.Message }],
            Message = "Ocorreu um erro inesperado."
        };
        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}
