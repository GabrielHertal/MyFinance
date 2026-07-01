namespace MyFinance.Domain.Common
{
    public class EntitidadeAuditavel : BaseEntidade
    {
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; private set; }
        protected void AtualizarDataAtualizacao()
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
