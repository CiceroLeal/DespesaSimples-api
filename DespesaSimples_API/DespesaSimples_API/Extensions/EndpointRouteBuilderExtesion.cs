﻿using DespesaSimples_API.Controllers;
using DespesaSimples_API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
    
    public static void RegisterCartaoEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var cartaoEndpoints = endpointRouteBuilder.MapGroup("/cartoes")
            .RequireAuthorization();
        var cartaoComIdEndpoints = cartaoEndpoints.MapGroup("/{cartaoId}");

        cartaoEndpoints.MapGet("", CartaoController.GetCartoesAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetCartoes");

        cartaoEndpoints.MapPost("", CartaoController.CriarCartaoAsync)
            .AddEndpointFilter<CartaoDtoValidationFilter>()
            .WithName("CreateCartoes");

        cartaoComIdEndpoints.MapGet("", CartaoController.GetCartaoPorIdAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetCartao");

        cartaoComIdEndpoints.MapDelete("", CartaoController.DeleteCartaoPorIdAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("DeleteCartoes");

        cartaoComIdEndpoints.MapPut("", CartaoController.AtualizarCartaoAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .AddEndpointFilter<CartaoDtoValidationFilter>()
            .WithName("UpdateCartoes");
    }
    
    public static void RegisterCategoriaEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var categoriaEndpoints = endpointRouteBuilder.MapGroup("/categorias")
            .RequireAuthorization();
        var categoriaComIdEndpoints = categoriaEndpoints.MapGroup("/{categoriaId}");

        categoriaEndpoints.MapGet("", CategoriaController.GetCategoriasAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetCategorias");

        categoriaEndpoints.MapPost("", CategoriaController.CriarCategoriaAsync)
            .AddEndpointFilter<CategoriaDtoValidationFilter>()
            .WithName("CreateCategoria");

        categoriaComIdEndpoints.MapGet("", CategoriaController.GetCategoriaPorIdAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetCategoria");

        categoriaComIdEndpoints.MapDelete("", CategoriaController.DeleteCategoriaPorIdAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("DeleteCategoria");

        categoriaComIdEndpoints.MapPut("", CategoriaController.AtualizarCategoriaAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .AddEndpointFilter<CategoriaDtoValidationFilter>()
            .WithName("UpdateCategoria");
    }
    
    public static void RegisterTagEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var tagEndpoints = endpointRouteBuilder.MapGroup("/tags")
            .RequireAuthorization();
        var tagComIdEndpoints = tagEndpoints.MapGroup("/{tagId:int}");

        tagEndpoints.MapGet("", TagController.GetTagsAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetTags");

        tagEndpoints.MapPost("", TagController.CriarTagAsync)
            .AddEndpointFilter<TagDtoValidationFilter>()
            .WithName("CreateTag");

        tagComIdEndpoints.MapGet("", TagController.GetTagPorIdAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetTag");

        tagComIdEndpoints.MapDelete("", TagController.DeleteTagPorIdAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("DeleteTag");

        tagComIdEndpoints.MapPut("", TagController.AtualizarTagAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .AddEndpointFilter<TagDtoValidationFilter>()
            .WithName("UpdateTag");
    }
    
    public static void RegisterBalancoEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var balancoEndpoints = endpointRouteBuilder.MapGroup("/balanco")
            .RequireAuthorization();

        balancoEndpoints.MapGet("", BalancoController.GetBalancoMesAnoAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetBalanco");
        
        balancoEndpoints.MapGet("/{ano:int}", BalancoController.GetBalancoAnoAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>()
            .WithName("GetBalancoAno");
    }
}