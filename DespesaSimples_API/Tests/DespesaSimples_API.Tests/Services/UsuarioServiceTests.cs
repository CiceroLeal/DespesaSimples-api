using System.Security.Claims;
using DespesaSimples_API.Dtos.Auth;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DespesaSimples_API.Tests.Services;

public class UsuarioServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public UsuarioServiceTests()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock =
            new Mock<UserManager<User>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["JWT:Secret"]).Returns("supersecretkey1234567890");
        _configurationMock.Setup(c => c["JWT:ValidIssuer"]).Returns("issuer");
        _configurationMock.Setup(c => c["JWT:ValidAudience"]).Returns("audience");
    }

    private UsuarioService CreateService() =>
        new(_userManagerMock.Object, _httpContextAccessorMock.Object, _configurationMock.Object);

    [Fact]
    public async Task RegisterAsync_DeveRetornarUsuarioResponseDtoComToken_QuandoSucesso()
    {
        var loginDto = new UsuarioCriacaoDto { Nome = "Teste", Email = "teste@email.com", Senha = "Senha123!" };
        var user = new User { Id = "1", Nome = "Teste", Email = "teste@email.com", UserName = "teste" };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), loginDto.Senha))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDto.Senha))
            .ReturnsAsync(true);
        
        _configurationMock.Setup(c => c["JWT:Secret"]).Returns("supersecretkey1234567890abcdef12345678");

        var service = CreateService();
        var result = await service.RegisterAsync(loginDto);

        Assert.NotNull(result.Token);
        Assert.Equal(loginDto.Email, result.Usuario.Email);
        Assert.Equal(loginDto.Nome, result.Usuario.Nome);
    }

    [Fact]
    public async Task RegisterAsync_DeveRetornarUsuarioResponseDtoComErros_QuandoFalha()
    {
        var loginDto = new UsuarioCriacaoDto { Nome = "Teste", Email = "teste@email.com", Senha = "Senha123!" };
        var identityErrors = new[] { new IdentityError { Description = "Erro" } };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), loginDto.Senha))
            .ReturnsAsync(IdentityResult.Failed(identityErrors));

        var service = CreateService();
        var result = await service.RegisterAsync(loginDto);

        Assert.Empty(result.Token);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarUsuarioResponseDtoComToken_QuandoSucesso()
    {
        var loginDto = new LoginDto { Email = "teste@email.com", Senha = "Senha123!" };
        var user = new User { Id = "1", Nome = "Teste", Email = "teste@email.com", UserName = "teste" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDto.Senha))
            .ReturnsAsync(true);
        _configurationMock.Setup(c => c["JWT:Secret"]).Returns("supersecretkey1234567890abcdef12345678");

        var service = CreateService();
        var result = await service.LoginAsync(loginDto);

        Assert.NotNull(result.Token);
        Assert.Equal(loginDto.Email, result.Usuario.Email);
        Assert.Equal(user.Nome, result.Usuario.Nome);
    }

    [Fact]
    public async Task LoginAsync_DeveLancarUnauthorizedAccessException_QuandoUsuarioNaoExiste()
    {
        var loginDto = new LoginDto { Email = "naoexiste@email.com", Senha = "Senha123!" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync((User?)null);

        var service = CreateService();

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(loginDto));
    }

    [Fact]
    public async Task LoginAsync_DeveLancarUnauthorizedAccessException_QuandoSenhaIncorreta()
    {
        var loginDto = new LoginDto { Email = "teste@email.com", Senha = "SenhaErrada!" };
        var user = new User { Id = "1", Nome = "Teste", Email = "teste@email.com" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDto.Senha))
            .ReturnsAsync(false);

        var service = CreateService();

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(loginDto));
    }

    [Fact]
    public async Task AtualizarUsuarioAsync_DeveRetornarUsuarioResponseDto_QuandoSucesso()
    {
        var usuarioAtualizacaoDto = new UsuarioAtualizacaoDto { Nome = "NovoNome", Email = "novo@email.com" };
        var user = new User { Id = "1", Nome = "AntigoNome", Email = "antigo@email.com" };

        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(MockHttpContextWithUserId("1"));
        _userManagerMock.Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.UpdateAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        var service = CreateService();
        var result = await service.AtualizarUsuarioAsync(usuarioAtualizacaoDto);

        Assert.Equal("NovoNome", result.Usuario.Nome);
        Assert.Equal("novo@email.com", result.Usuario.Email);
    }

    [Fact]
    public async Task AtualizarUsuarioAsync_DeveLancarUnauthorizedAccessException_QuandoUsuarioNaoEncontrado()
    {
        var usuarioAtualizacaoDto = new UsuarioAtualizacaoDto { Nome = "NovoNome", Email = "novo@email.com" };

        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(MockHttpContextWithUserId("1"));
        _userManagerMock.Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync((User?)null);

        var service = CreateService();

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.AtualizarUsuarioAsync(usuarioAtualizacaoDto));
    }

    [Fact]
    public async Task AlterarSenhaAsync_DeveRetornarUsuarioResponseDto_QuandoSucesso()
    {
        var dto = new UsuarioAlteracaoSenhaDto { SenhaAtual = "Senha123!", NovaSenha = "NovaSenha123!" };
        var user = new User { Id = "1", Nome = "Teste", Email = "teste@email.com" };

        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(MockHttpContextWithUserId("1"));
        _userManagerMock.Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.ChangePasswordAsync(user, dto.SenhaAtual, dto.NovaSenha))
            .ReturnsAsync(IdentityResult.Success);

        var service = CreateService();
        var result = await service.AlterarSenhaAsync(dto);

        Assert.Equal(user.Nome, result.Usuario.Nome);
        Assert.Equal(user.Email, result.Usuario.Email);
    }

    [Fact]
    public async Task AlterarSenhaAsync_DeveLancarUnauthorizedAccessException_QuandoUsuarioNaoEncontrado()
    {
        var dto = new UsuarioAlteracaoSenhaDto { SenhaAtual = "Senha123!", NovaSenha = "NovaSenha123!" };

        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(MockHttpContextWithUserId("1"));
        _userManagerMock.Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync((User?)null);

        var service = CreateService();

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.AlterarSenhaAsync(dto));
    }

    [Fact]
    public async Task BuscarUsuarioAtualAsync_DeveRetornarUsuarioResponseDto_QuandoSucesso()
    {
        var user = new User { Id = "1", Nome = "Teste", Email = "teste@email.com" };

        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(MockHttpContextWithUserId("1"));
        _userManagerMock.Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync(user);

        var service = CreateService();
        var result = await service.BuscarUsuarioAtualAsync();

        Assert.Equal(user.Nome, result.Usuario.Nome);
        Assert.Equal(user.Email, result.Usuario.Email);
    }

    [Fact]
    public async Task BuscarUsuarioAtualAsync_DeveLancarUnauthorizedAccessException_QuandoUsuarioNaoEncontrado()
    {
        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(MockHttpContextWithUserId("1"));
        _userManagerMock.Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync((User?)null);

        var service = CreateService();

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.BuscarUsuarioAtualAsync());
    }

    [Fact]
    public void GetIdUsuarioAtual_DeveRetornarId_QuandoClaimSubExiste()
    {
        var httpContext = MockHttpContextWithUserId("123");
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        var service = CreateService();
        var id = service.GetIdUsuarioAtual();

        Assert.Equal("123", id);
    }

    [Fact]
    public void GetIdUsuarioAtual_DeveLancarUnauthorizedAccessException_QuandoClaimNaoExiste()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        var service = CreateService();

        Assert.Throws<UnauthorizedAccessException>(() => service.GetIdUsuarioAtual());
    }

    private static HttpContext MockHttpContextWithUserId(string userId)
    {
        var claims = new[] { new Claim("sub", userId) };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };
        return context;
    }
}