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
        [FromBody] LoginDto loginDto
    )
    {
        var result = await usuarioService.RegisterAsync(loginDto);
        if (result.Errors.Any())
        {
            var errors = result.Errors.Select(e => new ApiError { Code = e.Code, Message = e.Description });
            return ApiResultsUtil.Error(errors, "Erro ao registrar usuário");
        }

        return ApiResultsUtil.Success(result, "Usuário registrado com sucesso");
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
            return ApiResultsUtil.Unauthorized("Usuário ou senha inválidos.");
        }
    }

    public static async Task<IResult> AtualizarUsuario(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] UsuarioAtualizacaoDto usuarioAtualizacaoDto
    )
    {
        var result = await usuarioService.AtualizarUsuarioAsync(usuarioAtualizacaoDto);

        if (result.Errors.Any())
        {
            var errors = result.Errors.Select(e => new ApiError { Code = e.Code, Message = e.Description });
            return ApiResultsUtil.Error(errors, "Erro ao atualizar usuário");
        }

        return ApiResultsUtil.Success(result, "Usuário atualizado com sucesso");
    }

    public static async Task<IResult> AlterarSenha(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] UsuarioAlteracaoSenhaDto usuarioAlteracaoSenhaDto
    )
    {
        try
        {
            var result = await usuarioService.AlterarSenhaAsync(usuarioAlteracaoSenhaDto);
            if (result.Errors.Any())
            {
                var errors = result.Errors.Select(e => new ApiError { Code = e.Code, Message = e.Description });
                return ApiResultsUtil.Error(errors, "Erro ao alterar senha");
            }

            return ApiResultsUtil.Success(result, "Senha alterada com sucesso");
        }
        catch (UnauthorizedAccessException)
        {
            return ApiResultsUtil.Unauthorized("Não autorizado para alterar senha.");
        }
    }

    public static async Task<IResult> ObterUsuarioAtual(
        [FromServices] IUsuarioService usuarioService
    )
    {
        try
        {
            var result = await usuarioService.ObterUsuarioAtualAsync();
            return ApiResultsUtil.Success(result, "Usuário obtido com sucesso");
        }
        catch (UnauthorizedAccessException)
        {
            return ApiResultsUtil.Unauthorized("Não autorizado para obter usuário.");
        }
    }
}