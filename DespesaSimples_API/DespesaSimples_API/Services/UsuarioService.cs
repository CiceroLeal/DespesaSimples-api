using DespesaSimples_API.Dtos.Auth;
using DespesaSimples_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DespesaSimples_API.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DespesaSimples_API.Services;

public partial class UsuarioService(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration)
    : IUsuarioService
{
    public async Task<ResponseDto> RegisterAsync(LoginDto loginDto)
    {
        var user = new User
        {
            Nome = loginDto.Nome ?? string.Empty,
            UserName = RegexUsername().Replace(loginDto.Nome ?? string.Empty, "").Trim().ToLower(),
            Email = loginDto.Email
        };

        var result = await userManager.CreateAsync(user, loginDto.Senha);

        if (!result.Succeeded)
        {
            return new ResponseDto
            {
                Errors = result.Errors
            };
        }

        return await LoginAsync(loginDto);
    }

    public async Task<ResponseDto> LoginAsync(LoginDto loginDto)
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

        return new ResponseDto
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
        var userId = httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("Usuário não autenticado");
        }

        return userId;
    }

    public async Task<ResponseDto> AtualizarUsuarioAsync(UsuarioAtualizacaoDto usuarioAtualizacaoDto)
    {
        var userId = GetIdUsuarioAtual();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado");
        }

        user.Nome = usuarioAtualizacaoDto.Nome;
        user.Email = usuarioAtualizacaoDto.Email;
        user.UserName = RegexUsername().Replace(usuarioAtualizacaoDto.Nome, "").Trim().ToLower();

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return new ResponseDto
            {
                Errors = result.Errors
            };
        }

        return new ResponseDto
        {
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Nome = user.Nome
            }
        };
    }

    public async Task<ResponseDto> AlterarSenhaAsync(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenhaDto)
    {
        var userId = GetIdUsuarioAtual();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado");
        }

        var result = await userManager.ChangePasswordAsync(user, usuarioAlteracaoSenhaDto.SenhaAtual,
            usuarioAlteracaoSenhaDto.NovaSenha);

        if (!result.Succeeded)
        {
            return new ResponseDto
            {
                Errors = result.Errors
            };
        }

        return new ResponseDto
        {
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Nome = user.Nome
            }
        };
    }

    public async Task<UsuarioDto> ObterUsuarioAtualAsync()
    {
        var userId = GetIdUsuarioAtual();
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado");
        }

        return new UsuarioDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Nome = user.Nome
        };
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex RegexUsername();
}