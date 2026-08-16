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
                .HasColumnType("varchar(100)")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Ativo)
                .HasColumnType("boolean")
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(c => c.DataCriacao)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(300)")
                .HasMaxLength(300);
            //Foregn Key
            builder.Property(c => c.UsuarioId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}