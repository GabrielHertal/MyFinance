using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Infrastructure.Persistence;
using MyFinance.Infrastructure.Persistence.Context;
using MyFinance.Infrastructure.Repositories;
using MyFinance.Infrastructure.Identity;
using MyFinance.Application.Interfaces.Services;

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
            var jwtSection = configuration.GetSection(JwtSettings.SectionName);
            var jwtSettings = jwtSection.Get<JwtSettings>()
                ?? throw new InvalidOperationException("A configuração Jwt é obrigatória.");
            if (Encoding.UTF8.GetByteCount(jwtSettings.Key) < 32)
                throw new InvalidOperationException("Jwt:Key deve possuir pelo menos 32 bytes.");

            services.Configure<JwtSettings>(jwtSection);
            services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<MyFinanceDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };
                });
            services.AddAuthorization();
            services.AddScoped<ITokenService, JwtTokenService>();
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
