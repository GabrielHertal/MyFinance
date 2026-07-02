using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Identity;

namespace MyFinance.Infrastructure.Persistence.Configurations
{
    public class ParcelamentoConfiguration : IEntityTypeConfiguration<Parcelamento>
    {
        public void Configure(EntityTypeBuilder<Parcelamento> builder)
        {
            builder.ToTable("Parcelamento");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Valor_Total)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.QuantidadeParcelas)
                .HasColumnType("int")
                .IsRequired();
            builder.Property(p => p.DataInicio)
                .HasColumnType("timestamp")
                .IsRequired();
            //Foregn Key
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}