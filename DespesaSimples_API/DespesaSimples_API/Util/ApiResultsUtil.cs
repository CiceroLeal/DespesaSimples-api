using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Util;

public static class ApiResultsUtil
{
    public static IResult Success<T>(T data, string? message) =>
        Results.Ok(new ApiResponse<T,List<ApiError>> { Success = true, Data = data, Message = message });
    
    public static IResult Success(string? message) =>
        Results.Ok(new ApiResponse<object,List<ApiError>> { Success = true, Message = message });

    public static IResult Unauthorized(string? message = null) =>
        Results.Json(
            new ApiResponse<object, List<ApiError>>
            {
                Success = false,
                Errors = [new ApiError { Code = "UNAUTHORIZED", Message = message ?? "Não autorizado" }]
            },
            statusCode: StatusCodes.Status401Unauthorized
        );
    
    public static IResult NotFound(string? message = null) =>
        Results.Json(
            new ApiResponse<object, List<ApiError>>
            {
                Success = false,
                Errors = [new ApiError { Code = "NOT_FOUND", Message = message ?? "Dados não encontrados" }]
            },
            statusCode: StatusCodes.Status404NotFound
        );
    
    public static IResult BadRequest<T>(T errors) =>
        Results.Json(
            new ApiResponse<object, T>
            {
                Success = false,
                Message = "Dados inválidos",
                Errors = errors
            },
            statusCode: StatusCodes.Status400BadRequest
        );
    
    public static IResult BadRequest(string? message = null) =>
        Results.Json(
            new ApiResponse<object, List<ApiError>>
            {
                Success = false,
                Errors = [new ApiError { Code = "BAD_REQUEST", Message = message ?? "Erro de processamento" }]
            },
            statusCode: StatusCodes.Status400BadRequest
        );
    
    public static IResult BadRequest<T>(T data, string? message) =>
        Results.Json(
            new ApiResponse<object, List<ApiError>>
            {
                Success = false,
                Data = data,
                Errors = [new ApiError { Code = "BAD_REQUEST", Message = message ?? "Erro de processamento" }]
            },
            statusCode: StatusCodes.Status400BadRequest
        );
}