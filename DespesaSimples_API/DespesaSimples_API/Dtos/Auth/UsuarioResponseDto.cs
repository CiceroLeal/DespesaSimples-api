using Microsoft.AspNetCore.Identity;

namespace DespesaSimples_API.Dtos.Auth;

public class UsuarioResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UsuarioDto Usuario { get; set; } = new ();
    public IEnumerable<IdentityError> Errors { get; set; } = new List<IdentityError>();
}