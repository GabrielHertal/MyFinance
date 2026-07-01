using MyFinance.Domain.Common;

namespace MyFinance.Domain.Entities
{
    public class Categoria : EntitidadeAuditavel
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public Guid UsuarioId { get; private set; }
        public bool Ativo { get; set; } = true;
        public Categoria(string nome, string descricao, Guid usuarioId)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria não pode ser vazio!", nameof(nome));
            if (usuarioId == Guid.Empty)
                throw new ArgumentNullException("O Id do usuário não pode ser vazio!", nameof(usuarioId));
            Nome = nome;
            Descricao = descricao;
            UsuarioId = usuarioId;
        }
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
        public void Atualizar(string nome, string descricao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria não pode ser vazio!", nameof(nome));
            Nome = nome;
            Descricao = descricao;
            AtualizarDataAtualizacao();
        }   
        protected Categoria()
        {
        }
    }
}