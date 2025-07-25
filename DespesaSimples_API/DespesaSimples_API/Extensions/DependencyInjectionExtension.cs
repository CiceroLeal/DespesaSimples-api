using DespesaSimples_API.Abstractions.Infra;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Infra;
using DespesaSimples_API.Repositories;
using DespesaSimples_API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DespesaSimples_API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCustomDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            services.AddScoped<ITransacaoFixaRepository, TransacaoFixaRepository>();
            services.AddScoped<ITransacaoFixaService, TransacaoFixaService>();

            services.AddScoped<ICartaoRepository, CartaoRepository>();
            services.AddScoped<ICartaoService, CartaoService>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IBalancoRepository, BalancoRepository>();
            services.AddScoped<IBalancoService, BalancoService>();

            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<ITransactionManager, TransactionManager>();
            return services;
        }
    }
}