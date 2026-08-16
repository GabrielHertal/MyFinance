using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Identity;

namespace MyFinance.Infrastructure.Persistence.Configurations
{
    public class CartaoConfiguration : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        } 
    }
}
