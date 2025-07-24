using AutoFixture;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Controllers;
using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DespesaSimples_API.Tests.Controllers;

public class UsuarioControllerTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Register_DeveRetornarSuccess_QuandoNaoHaErros()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var loginDto = _fixture.Create<UsuarioCriacaoDto>();
        var resultado = new UsuarioResponseDto { Errors = [] };
        usuarioService.Setup(s => s.RegisterAsync(loginDto)).ReturnsAsync(resultado);

        var result = await UsuarioController.Register(usuarioService.Object, loginDto);

        Assert.IsType<Ok<ApiResponseDto<UsuarioResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task Register_DeveRetornarBadRequest_QuandoHaErros()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var loginDto = _fixture.Create<UsuarioCriacaoDto>();
        var resultado = new UsuarioResponseDto
        {
            Errors = [new IdentityError { Code = "ERR", Description = "Falha" }]
        };
        usuarioService.Setup(s => s.RegisterAsync(loginDto)).ReturnsAsync(resultado);

        var result = await UsuarioController.Register(usuarioService.Object, loginDto);

        Assert.IsType<JsonHttpResult<ApiResponseDto<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task Login_DeveRetornarSuccess_QuandoLoginValido()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var loginDto = _fixture.Create<LoginDto>();
        var resultado = new UsuarioResponseDto();
        usuarioService.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(resultado);

        var result = await UsuarioController.Login(usuarioService.Object, loginDto);

        Assert.IsType<Ok<ApiResponseDto<UsuarioResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task Login_DeveRetornarUnauthorized_QuandoLoginInvalido()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var loginDto = _fixture.Create<LoginDto>();
        usuarioService.Setup(s => s.LoginAsync(loginDto)).ThrowsAsync(new UnauthorizedAccessException());

        var result = await UsuarioController.Login(usuarioService.Object, loginDto);

        Assert.IsType<JsonHttpResult<ApiResponseDto<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AtualizarUsuario_DeveRetornarSuccess_QuandoNaoHaErros()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var dto = _fixture.Create<UsuarioAtualizacaoDto>();
        var resultado = new UsuarioResponseDto { Errors = [] };
        usuarioService.Setup(s => s.AtualizarUsuarioAsync(dto)).ReturnsAsync(resultado);

        var result = await UsuarioController.AtualizarUsuario(usuarioService.Object, dto);

        Assert.IsType<Ok<ApiResponseDto<UsuarioResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AtualizarUsuario_DeveRetornarBadRequest_QuandoHaErros()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var dto = _fixture.Create<UsuarioAtualizacaoDto>();
        var resultado = new UsuarioResponseDto
        {
            Errors = [new IdentityError { Code = "ERR", Description = "Falha" }]
        };
        usuarioService.Setup(s => s.AtualizarUsuarioAsync(dto)).ReturnsAsync(resultado);

        var result = await UsuarioController.AtualizarUsuario(usuarioService.Object, dto);

        Assert.IsType<JsonHttpResult<ApiResponseDto<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AlterarSenha_DeveRetornarSuccess_QuandoNaoHaErros()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var dto = _fixture.Create<UsuarioAlteracaoSenhaDto>();
        var resultado = new UsuarioResponseDto { Errors = [] };
        usuarioService.Setup(s => s.AlterarSenhaAsync(dto)).ReturnsAsync(resultado);

        var result = await UsuarioController.AlterarSenha(usuarioService.Object, dto);

        Assert.IsType<Ok<ApiResponseDto<UsuarioResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AlterarSenha_DeveRetornarBadRequest_QuandoHaErros()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var dto = _fixture.Create<UsuarioAlteracaoSenhaDto>();
        var resultado = new UsuarioResponseDto
        {
            Errors = [new IdentityError { Code = "ERR", Description = "Falha" }]
        };
        usuarioService.Setup(s => s.AlterarSenhaAsync(dto)).ReturnsAsync(resultado);

        var result = await UsuarioController.AlterarSenha(usuarioService.Object, dto);

        Assert.IsType<JsonHttpResult<ApiResponseDto<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AlterarSenha_DeveRetornarUnauthorized_QuandoNaoAutorizado()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var dto = _fixture.Create<UsuarioAlteracaoSenhaDto>();
        usuarioService.Setup(s => s.AlterarSenhaAsync(dto)).ThrowsAsync(new UnauthorizedAccessException());

        var result = await UsuarioController.AlterarSenha(usuarioService.Object, dto);

        Assert.IsType<JsonHttpResult<ApiResponseDto<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task BuscarUsuarioAtual_DeveRetornarSuccess_QuandoAutorizado()
    {
        var usuarioService = new Mock<IUsuarioService>();
        var resultado = _fixture.Create<UsuarioResponseDto>();
        usuarioService.Setup(s => s.BuscarUsuarioAtualAsync()).ReturnsAsync(resultado);

        var result = await UsuarioController.BuscarUsuarioAtual(usuarioService.Object);

        Assert.IsType<Ok<ApiResponseDto<UsuarioResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task BuscarUsuarioAtual_DeveRetornarUnauthorized_QuandoNaoAutorizado()
    {
        var usuarioService = new Mock<IUsuarioService>();
        usuarioService.Setup(s => s.BuscarUsuarioAtualAsync()).ThrowsAsync(new UnauthorizedAccessException());

        var result = await UsuarioController.BuscarUsuarioAtual(usuarioService.Object);

        Assert.IsType<JsonHttpResult<ApiResponseDto<object, List<ApiError>>>>(result);
    }
}