using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Infrastructure.Persistence;
using MyFinance.Infrastructure.Persistence.Context;
using MyFinance.Infrastructure.Repositories;

namespace MyFinance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyFinanceDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICartaoRepository, CartaoRepository>();
            services.AddScoped<IParcelamentoRepository, ParcelamentoRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            return services;
        }
    }
}