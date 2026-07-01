using MyFinance.Domain.Common;
using MyFinance.Domain.Enums;

namespace MyFinance.Domain.Entities
{
    public class Transacao : EntitidadeAuditavel
    {
        public Guid ContaId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid? ParcelaId { get; private set; }
        public int NumeroParcela { get; private set; } = 1;
        public int TotalParcelas { get; private set; } = 1;
        public Guid UsuarioId { get; private set; } = default!;
        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public DateTime DataTransacao { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public StatusTransacao Status { get; private set; } = StatusTransacao.Pendente;
        public Transacao(Guid contaId, Guid categoriaId, Guid usuarioId, string descricao, decimal valor, TipoTransacao tipo, DateTime dataTransacao, DateTime? dataPagamento = null
                            , StatusTransacao status = StatusTransacao.Pendente)
        {
            if (contaId == Guid.Empty)
                throw new ArgumentException("O Id da conta não pode ser vazio!", nameof(contaId));
            if (categoriaId == Guid.Empty)
                throw new ArgumentException("O Id da categoria não pode ser vazio!", nameof(categoriaId));
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("O Id do usuário não pode ser vazio!", nameof(usuarioId));
            if (valor <= 0)
                throw new ArgumentException("O valor da transação deve ser positivo!", nameof(valor));
            ContaId = contaId;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            DataTransacao = dataTransacao;
            DataPagamento = dataPagamento;
            Status = status;
        }
        public void Pagar()
        {
            if (Status == StatusTransacao.Pago)
                throw new InvalidOperationException("A transação já está marcada como paga!");
            Status = StatusTransacao.Pago;
            DataPagamento = DateTime.Now;
            AtualizarDataAtualizacao();
        }
        public void Cancelar()
        {
            if (Status == StatusTransacao.Pago)
                throw new InvalidOperationException("A transação já está marcada como paga e não pode ser cancelada!"); 
            if (Status == StatusTransacao.Cancelado)
                throw new InvalidOperationException("A transação já está marcada como cancelada!");
            Status = StatusTransacao.Cancelado;
            AtualizarDataAtualizacao();
        }
        public void Estornar()
        {
            if (Status == StatusTransacao.Pago)
                throw new InvalidOperationException("A transação já está marcada como paga e não pode ser estornada!");
            if (Status == StatusTransacao.Estornado)
                throw new InvalidOperationException("A transação já está marcada como estornada!");
            Status = StatusTransacao.Estornado;
            AtualizarDataAtualizacao();
        }
        public void AtualizarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição da transação não pode ser vazia!", nameof(descricao));
            Descricao = descricao;
            AtualizarDataAtualizacao();
        }
        public void AtualizarCategoria(Guid categoriaId)
        {
            if (categoriaId == Guid.Empty)
                throw new ArgumentException("O Id da categoria não pode ser vazio!", nameof(categoriaId));
            CategoriaId = categoriaId;
            AtualizarDataAtualizacao();
        }
        public void AtualizarValor(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor da transação deve ser positivo!", nameof(valor));
            Valor = valor;
            AtualizarDataAtualizacao();
        }
        public void AtualizarConta(Guid contaId)
        {
            if (contaId == Guid.Empty)
                throw new ArgumentException("O Id da conta não pode ser vazio!", nameof(contaId));
            ContaId = contaId;
            AtualizarDataAtualizacao();
        }
        public void AtualizarDataTransacao(DateTime dataTransacao)
        {
            DataTransacao = dataTransacao;
            AtualizarDataAtualizacao();
        }
        public void AtualizarParcelas(int quantidadeParcelas)
        {
            if (quantidadeParcelas <= 0)
                throw new ArgumentException("A quantidade de parcelas deve ser positiva!", nameof(quantidadeParcelas));
            TotalParcelas = quantidadeParcelas;
            AtualizarDataAtualizacao();
        }
        protected Transacao()
        {
        }
    }
}