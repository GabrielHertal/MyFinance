using MyFinance.Domain.Common;

namespace MyFinance.Domain.Entities
{
    public class Cartao : EntitidadeAuditavel
    {
        public string Nome { get; private set; } = string.Empty;
        public string Banco { get; private set; } = string.Empty;
        public string Numero_Final { get; private set; } = string.Empty;
        public Guid UsuarioId { get; private set; }
        public Cartao(string nome, string banco, string numeroFinal, Guid usuarioId)
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
            Nome = nome;
            Banco = banco;
            UsuarioId = usuarioId;
            Numero_Final = numeroFinal;
        }
        protected Cartao()
        { 
        }
    }
}