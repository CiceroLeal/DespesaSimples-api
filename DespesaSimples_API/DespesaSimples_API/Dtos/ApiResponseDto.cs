namespace DespesaSimples_API.Dtos;

public class ApiResponseDto<T, E>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public E? Errors { get; set; }
    public string? Message { get; set; }
}

public class ApiError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
} 