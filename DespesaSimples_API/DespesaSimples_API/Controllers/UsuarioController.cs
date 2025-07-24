using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Auth;
using DespesaSimples_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DespesaSimples_API.Controllers;

public static class UsuarioController
{
    public static async Task<IResult> Register(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] UsuarioCriacaoDto loginDto
    )
    {
        var result = await usuarioService.RegisterAsync(loginDto);
        
        return result.Errors.Any() 
            ? ApiResultsUtil.BadRequest(result, "Erro ao registrar usuário") 
            : ApiResultsUtil.Success(result, "Usuário registrado com sucesso");
    }

    public static async Task<IResult> Login(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] LoginDto loginDto
    )
    {
        try
        {
            var result = await usuarioService.LoginAsync(loginDto);
            return ApiResultsUtil.Success(result, "Login realizado com sucesso");
        }
        catch (UnauthorizedAccessException)
        {
            return ApiResultsUtil.Unauthorized("Usuário ou senha inválidos");
        }
    }

    public static async Task<IResult> AtualizarUsuario(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] UsuarioAtualizacaoDto usuarioAtualizacaoDto
    )
    {
        var result = await usuarioService.AtualizarUsuarioAsync(usuarioAtualizacaoDto);

        return result.Errors.Any() 
            ? ApiResultsUtil.BadRequest(result, "Erro ao atualizar usuário") 
            : ApiResultsUtil.Success(result, "Usuário atualizado com sucesso");
    }

    public static async Task<IResult> AlterarSenha(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] UsuarioAlteracaoSenhaDto usuarioAlteracaoSenhaDto
    )
    {
        try
        {
            var result = await usuarioService.AlterarSenhaAsync(usuarioAlteracaoSenhaDto);
            return result.Errors.Any() 
                ? ApiResultsUtil.BadRequest(result, "Erro ao alterar senha") 
                : ApiResultsUtil.Success(result, "Senha alterada com sucesso");
        }
        catch (UnauthorizedAccessException)
        {
            return ApiResultsUtil.Unauthorized("Não autorizado para alterar senha");
        }
    }

    public static async Task<IResult> BuscarUsuarioAtual(
        [FromServices] IUsuarioService usuarioService
    )
    {
        try
        {
            var result = await usuarioService.BuscarUsuarioAtualAsync();
            return ApiResultsUtil.Success(result, "Usuário obtido com sucesso");
        }
        catch (UnauthorizedAccessException)
        {
            return ApiResultsUtil.Unauthorized("Não autorizado para obter usuário");
        }
    }
}