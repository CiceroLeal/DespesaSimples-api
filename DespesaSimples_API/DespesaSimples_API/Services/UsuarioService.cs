using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Auth;
using DespesaSimples_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DespesaSimples_API.Services;

public partial class UsuarioService(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration)
    : IUsuarioService
{
    public async Task<UsuarioResponseDto> RegisterAsync(UsuarioCriacaoDto registroDto)
    {
        var user = new User
        {
            Nome = registroDto.Nome ?? string.Empty,
            UserName = RegexUsername().Replace(registroDto.Nome ?? string.Empty, "").Trim().ToLower(),
            Email = registroDto.Email
        };

        var result = await userManager.CreateAsync(user, registroDto.Senha);

        if (!result.Succeeded)
        {
            return new UsuarioResponseDto
            {
                Errors = result.Errors
            };
        }
        
        return await LoginAsync(new LoginDto
        {
            Email = registroDto.Email,
            Senha = registroDto.Senha
        });
    }

    public async Task<UsuarioResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Senha))
        {
            throw new UnauthorizedAccessException("Email ou senha inválidos");
        }

        var authClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var authSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? string.Empty));

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new UsuarioResponseDto
        {
            Token = tokenString,
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                Email = loginDto.Email,
                Nome = user.Nome
            }
        };
    }

    public string GetIdUsuarioAtual()
    {
        var userId = httpContextAccessor
            .HttpContext?
            .User
            .FindFirst("sub")?
            .Value;

        if (string.IsNullOrEmpty(userId))
            userId = httpContextAccessor
                .HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return userId;
    }

    public async Task<UsuarioResponseDto> AtualizarUsuarioAsync(UsuarioAtualizacaoDto usuarioAtualizacaoDto)
    {
        var userId = GetIdUsuarioAtual();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado");

        user.Nome = usuarioAtualizacaoDto.Nome;
        user.Email = usuarioAtualizacaoDto.Email;
        user.UserName = RegexUsername()
            .Replace(usuarioAtualizacaoDto.Nome, "")
            .Trim()
            .ToLower();

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return new UsuarioResponseDto
            {
                Errors = result.Errors
            };
        }

        return new UsuarioResponseDto
        {
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Nome = user.Nome
            }
        };
    }

    public async Task<UsuarioResponseDto> AlterarSenhaAsync(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenhaDto)
    {
        var userId = GetIdUsuarioAtual();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado");

        var result = await userManager.ChangePasswordAsync(user, usuarioAlteracaoSenhaDto.SenhaAtual,
            usuarioAlteracaoSenhaDto.NovaSenha);

        if (!result.Succeeded)
        {
            return new UsuarioResponseDto
            {
                Errors = result.Errors
            };
        }

        return new UsuarioResponseDto
        {
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Nome = user.Nome
            }
        };
    }

    public async Task<UsuarioResponseDto> ObterUsuarioAtualAsync()
    {
        var userId = GetIdUsuarioAtual();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado");

        return new UsuarioResponseDto
        {
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Nome = user.Nome
            }
        };
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex RegexUsername();
}