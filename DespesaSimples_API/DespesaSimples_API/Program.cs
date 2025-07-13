using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Extensions;
using DespesaSimples_API.Factories;
using DespesaSimples_API.Repositories;
using DespesaSimples_API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IModelCacheKeyFactory, UserModelCacheKeyFactory>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DespesaSimplesDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(9, 1, 0))
    );
});

builder.Services.AddIdentity<User, IdentityRole>(options => { options.User.RequireUniqueEmail = true; })
    .AddEntityFrameworkStores<DespesaSimplesDbContext>()
    .AddErrorDescriber<IdentityPortugueseMessagesExtension>()
    .AddDefaultTokenProviders();

// Configuração do Swagger
builder.Services.AddCustomSwagger();

// Configuração de dependências
builder.Services.AddCustomDependencies();

// Configuração de CORS
builder.Services.AddCustomCors();

// Configuração de autenticação/autorização
builder.Services.AddCustomAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Despesa Simples API - v1"); });
}

app.UseCors("AllowFrontend");
app.UseCors("AllowLocalNetwork");

app.UseAuthentication();
app.UseAuthorization();

// Adiciona endpoints
app.RegisterAuthEndpoints();
app.RegisterTagEndpoints();
app.RegisterBalancoEndpoints();
app.RegisterCategoriaEndpoints();

app.Run();