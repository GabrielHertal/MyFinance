using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyFinance.Infrastructure.Identity;
using MyFinance.Domain.Entities;

namespace MyFinance.Infrastructure.Persistence.Context
{
    public class MyFinanceDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>,Guid>
    {
        public MyFinanceDbContext(DbContextOptions<MyFinanceDbContext> options) 
            : base(options) 
        {
        }
        public DbSet<Conta> Conta => Set<Conta>();
        public DbSet<Cartao> Cartao => Set<Cartao>();
        public DbSet<Transacao> Transacao => Set<Transacao>();  
        public DbSet<Categoria> Categoria => Set<Categoria>();
        public DbSet<Parcelamento> Parcelamento => Set<Parcelamento>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(MyFinanceDbContext).Assembly);
        }
    }
}