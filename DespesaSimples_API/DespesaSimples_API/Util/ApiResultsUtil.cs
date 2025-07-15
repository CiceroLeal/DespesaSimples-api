using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Util;

public static class ApiResultsUtil
{
    public static IResult Success<T>(T data, string? message = null) =>
        Results.Ok(new ApiResponse<T> { Success = true, Data = data, Message = message });

    public static IResult Unauthorized(string? message = null) =>
        Results.Json(
            new ApiResponse<object>
            {
                Success = false,
                Errors = [new ApiError { Code = "UNAUTHORIZED", Message = message ?? "Não autorizado" }]
            },
            statusCode: StatusCodes.Status401Unauthorized
        );
    
    public static IResult NotFound(string? message = null) =>
        Results.Json(
            new ApiResponse<object>
            {
                Success = false,
                Errors = [new ApiError { Code = "NOT_FOUND", Message = message ?? "Dados não encontrados" }]
            },
            statusCode: StatusCodes.Status404NotFound
        );
    
    public static IResult BadRequest(string? message = null, List<ApiError>? errors = null) =>
        Results.Json(
            new ApiResponse<object>
            {
                Success = false,
                Errors = errors ?? 
                         [new ApiError { Code = "BAD_REQUEST", Message = message ?? "Erro de processamento" }]
            },
            statusCode: StatusCodes.Status400BadRequest
        );
    
    public static IResult BadRequest<T>(T data, string? message = null) =>
        Results.Json(
            new ApiResponse<object>
            {
                Success = false,
                Data = data,
                Errors = [new ApiError { Code = "BAD_REQUEST", Message = message ?? "Erro de processamento" }]
            },
            statusCode: StatusCodes.Status400BadRequest
        );
}