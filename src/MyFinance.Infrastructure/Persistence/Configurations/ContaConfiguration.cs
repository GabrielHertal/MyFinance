using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Identity;

namespace MyFinance.Infrastructure.Persistence.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Conta");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Saldo)
                .IsRequired()
                .HasColumnType("decimal(15,2)")
                .HasDefaultValue(0.00);
            builder.Property(c => c.Tipo)
                .HasColumnType("int")
                .HasConversion<int>()
                .IsRequired();
            builder.Property(c => c.Ativo)
                .HasColumnType("boolean")
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(c => c.DataCriacao)
                .HasColumnType("timestamp")
                .HasDefaultValue(DateTime.UtcNow);
            builder.HasMany<ApplicationUser>()
                .WithOne()
                .HasForeignKey(c => c.Id);
            builder.Property(c => c.UsuarioId)
                .IsRequired();
        }
    }
}