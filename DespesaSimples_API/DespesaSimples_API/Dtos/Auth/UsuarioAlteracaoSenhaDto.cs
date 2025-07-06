namespace DespesaSimples_API.Dtos.Auth;

public class UsuarioAlteracaoSenhaDto
{
    public required string SenhaAtual { get; set; }
    public required string NovaSenha { get; set; }
} 