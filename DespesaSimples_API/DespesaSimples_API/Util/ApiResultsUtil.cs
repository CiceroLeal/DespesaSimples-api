using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Util;

public static class ApiResultsUtil
{
    public static IResult Success<T>(T data, string? message = null) =>
        Results.Ok(new ApiResponse<T> { Success = true, Data = data, Message = message });

    public static IResult Error(IEnumerable<ApiError> errors, string? message = null,
        int statusCode = StatusCodes.Status400BadRequest) =>
        Results.Json(
            new ApiResponse<object> { Success = false, Errors = errors.ToList(), Message = message },
            statusCode: statusCode
        );

    public static IResult Unauthorized(string? message = null) =>
        Results.Json(
            new ApiResponse<object>
            {
                Success = false,
                Errors = [new ApiError { Code = "UNAUTHORIZED", Message = message ?? "Não autorizado." }],
                Message = message ?? "Não autorizado."
            },
            statusCode: StatusCodes.Status401Unauthorized
        );
}