using DespesaSimples_API.Dtos.Auth;

namespace DespesaSimples_API.Abstractions.Services;

public interface IUsuarioService
{
    Task<ResponseDto> RegisterAsync(LoginDto loginDto);
    Task<ResponseDto> LoginAsync(LoginDto loginDto);
    string GetIdUsuarioAtual();
    Task<ResponseDto> AtualizarUsuarioAsync(UsuarioAtualizacaoDto usuarioAtualizacaoDto);
    Task<ResponseDto> AlterarSenhaAsync(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenhaDto);
    Task<UsuarioDto> ObterUsuarioAtualAsync();
} 