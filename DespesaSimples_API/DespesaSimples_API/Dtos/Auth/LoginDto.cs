namespace DespesaSimples_API.Dtos.Auth;

public class LoginDto
{
    public string? Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
} 