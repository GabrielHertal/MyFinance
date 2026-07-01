using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Identity;

namespace MyFinance.Infrastructure.Persistence.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(t => t.NumeroParcela)
                .HasColumnType("int")
                .IsRequired();
            builder.Property(t => t.TotalParcelas)
                .HasColumnType("int")
                .IsRequired();
            builder.Property(t => t.UsuarioId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(t => t.Descricao)
                .HasColumnType("varchar(200)")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(t => t.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.Tipo)
                .HasColumnType("int")
                .HasComment("Tipo Transação: 1 - Deposito, 2 - Receita, 3 - Investimento")
                .IsRequired();
            builder.Property(t => t.DataTransacao)
                .HasColumnType("timestamp")
                .IsRequired();
            builder.Property(t => t.DataPagamento)
                .HasColumnType("timestamp");
            builder.Property(t => t.Status)
                .HasColumnType("int")
                .HasComment("Status: 0 - Pendente, 1 - Pago, 2 - Cancelado, 3 - Estornado")
                .IsRequired();
            //Foregn Keys
            builder.Property(t => t.UsuarioId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(t => t.ContaId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(t => t.CategoriaId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(t => t.ParcelaId)
                .HasColumnType("uuid");
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Conta>()
                .WithMany()
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Categoria>()
                .WithMany()
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}