using System.Reflection;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Extensions;
using DespesaSimples_API.Factories;
using DespesaSimples_API.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

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

app.UseMiddleware<BadJsonRequestMiddleware>();

// Adiciona endpoints
app.RegisterAuthEndpoints();
app.RegisterTransacaoEndpoints();
app.RegisterTransacaoFixaEndpoints();
app.RegisterTagEndpoints();
app.RegisterBalancoEndpoints();
app.RegisterCategoriaEndpoints();
app.RegisterCartaoEndpoints();

app.Run();