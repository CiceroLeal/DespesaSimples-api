using DespesaSimples_API.Dtos.Auth;

namespace DespesaSimples_API.Abstractions.Services;

public interface IUsuarioService
{
    Task<UsuarioResponseDto> RegisterAsync(UsuarioCriacaoDto registroDto);
    Task<UsuarioResponseDto> LoginAsync(LoginDto loginDto);
    string GetIdUsuarioAtual();
    Task<UsuarioResponseDto> AtualizarUsuarioAsync(UsuarioAtualizacaoDto usuarioAtualizacaoDto);
    Task<UsuarioResponseDto> AlterarSenhaAsync(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenhaDto);
    Task<UsuarioResponseDto> ObterUsuarioAtualAsync();
}