using Microsoft.Extensions.DependencyInjection;

namespace DespesaSimples_API.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://192.168.15.10:3001")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                options.AddPolicy("AllowLocalNetwork",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            return services;
        }
    }
}

