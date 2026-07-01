using MyFinance.Domain.Common;
using MyFinance.Domain.Enums;

namespace MyFinance.Domain.Entities
{
    public class Conta : EntitidadeAuditavel
    {
        public string Nome { get; private set; } = default!;
        public decimal Saldo { get; private set; }
        public Guid UsuarioId { get; private set; }
        public TipoConta Tipo { get; private set; }
        public bool Ativo { get; private set; } = true;
        public void Ativar()
        {
            Ativo = true;
            AtualizarDataAtualizacao();
        }
        public void Desativar()
        {
            Ativo = false;
            AtualizarDataAtualizacao();
        }
        public void Deposito(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("O valor do deposito deve ser positivo!", nameof(value));
            Saldo += value;
            AtualizarDataAtualizacao();
        }
        public void Saque(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("O valor do saque deve ser positivo!", nameof(value));
            if (value > Saldo)
                throw new InvalidOperationException("Saldo insuficiente para realizar o saque!");
            Saldo -= value;
            AtualizarDataAtualizacao();
        }
        public Conta(Guid usuarioId, string nome, decimal saldo, TipoConta tipoConta)
        {
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("O Id do usuário não pode ser vazio!", nameof(usuarioId));
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da conta não pode ser vazio!", nameof(nome));
            if (saldo <= 0)
                throw new ArgumentException("O saldo da conta deve ser positivo!", nameof(saldo));
            UsuarioId = usuarioId;
            Nome = nome;
            Saldo = saldo;
            Tipo = tipoConta;
            Ativo = true;
        }
        protected Conta()
        {
        }
    }
}