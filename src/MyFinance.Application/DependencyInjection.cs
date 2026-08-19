using Microsoft.Extensions.DependencyInjection;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Application.Services;

namespace MyFinance.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            return services;
        }
    }
}