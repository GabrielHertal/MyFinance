using MyFinance.Domain.Common;

namespace MyFinance.Domain.Entities
{
    public class Cartao : EntitidadeAuditavel
    {
        public string Nome { get; private set; } = string.Empty;
        public string Banco { get; private set; } = string.Empty;
        public decimal Limite { get; private set; } = 0;
        public string Numero_Final { get; private set; } = string.Empty;
        public Guid UsuarioId { get; private set; }
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
        public void Atualizar(string nome, string banco, string numeroFinal, decimal limite)
        {
            if (banco == string.Empty)
            {
                throw new ArgumentNullException("O nome do banco não pode ser vazio!");
            }
            if (nome == string.Empty)
            {
                throw new ArgumentNullException("O nome do cartão não pode ser vazio!");
            }
            if (numeroFinal == string.Empty)
            {
                throw new ArgumentNullException("O número final do cartão não pode ser vazio!");
            }
            if(limite <= 0)
            {
                throw new ArgumentNullException("O limite do cartão não pode ser zero ou negativo!");
            }
            Nome = nome;
            Banco = banco;
            Numero_Final = numeroFinal;
            Limite = limite;
            AtualizarDataAtualizacao();
        }
        public Cartao(string nome, string banco, string numeroFinal, Guid usuarioId, decimal limite)
        {
            if (banco == string.Empty)
            {
                throw new ArgumentNullException("O nome do banco não pode ser vazio!");
            }
            if (nome == string.Empty)
            {
                throw new ArgumentNullException("O nome do cartão não pode ser vazio!");
            }
            if (numeroFinal == string.Empty)
            {
                throw new ArgumentNullException("O número final do cartão não pode ser vazio!");
            }
            if (usuarioId == Guid.Empty)
            {
                throw new ArgumentNullException("O Id do usuário não pode ser vazio!");
            }
            if(limite <= 0)
            {
                throw new ArgumentNullException("O limite do cartão não pode ser zero ou negativo!");
            }
            Nome = nome;
            Banco = banco;
            UsuarioId = usuarioId;
            Numero_Final = numeroFinal;
            Limite = limite;
            Ativo = true;
        }
        protected Cartao()
        { 
        }
    }
}