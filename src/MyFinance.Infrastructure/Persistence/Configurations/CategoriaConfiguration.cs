using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Identity;

namespace MyFinance.Infrastructure.Persistence.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure (EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnType("uuid");
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(c => c.DataCriacao)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(c => c.Descricao)
                .HasMaxLength(300);
            builder.HasMany<ApplicationUser>()
                .WithOne()
                .HasForeignKey(c => c.Id);
            builder.Property(c => c.UsuarioId)
                .IsRequired();
        }
    }
}