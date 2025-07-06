using DespesaSimples_API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DespesaSimples_API.Extensions;

public static class EndpointRouteBuilderExtension
{
    public static void RegisterAuthEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authEndpoints = endpointRouteBuilder.MapGroup("/usuarios");

        authEndpoints.MapPost("/registro", UsuarioController.Register)
            .AllowAnonymous()
            .WithName("Registro");

        authEndpoints.MapPost("/login", UsuarioController.Login)
            .AllowAnonymous()
            .WithName("Login");

        authEndpoints.MapGet("", UsuarioController.ObterUsuarioAtual)
            .RequireAuthorization()
            .WithName("GetUsuario");

        authEndpoints.MapPut("", UsuarioController.AtualizarUsuario)
            .RequireAuthorization()
            .WithName("UpdateUsuario");

        authEndpoints.MapPut("/senha", UsuarioController.AlterarSenha)
            .RequireAuthorization()
            .WithName("UpdateSenha");
    }
}