using MyFinance.Domain.Common;

namespace MyFinance.Domain.Entities
{
    public class Parcelamento : EntitidadeAuditavel
    {
        public string Nome { get; private set; } = default!;
        public decimal Valor_Total { get; private set; }
        public int QuantidadeParcelas { get; private set; }
        public DateTime DataInicio { get; private set; }
        public Parcelamento(string nome, decimal valor_total, int quantidadeParcelas, DateTime dataInicio)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do parcelamento não pode ser vazio!", nameof(nome));
            if (valor_total <= 0)
                throw new ArgumentException("O valor do parcelamento deve ser positivo!", nameof(valor_total));
            if (quantidadeParcelas <= 0)
                throw new ArgumentException("A quantidade de parcelas deve ser positiva!", nameof(quantidadeParcelas));
            if (dataInicio < DateTime.UtcNow.Date)
                throw new ArgumentException("A data de início não pode ser no passado!", nameof(dataInicio));
            Nome = nome;
            Valor_Total = valor_total;
            QuantidadeParcelas = quantidadeParcelas;
            DataInicio = dataInicio;
        }
        public void Atualizar(string nome, decimal valor_total, int quantidadeParcelas, DateTime dataInicio)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do parcelamento não pode ser vazio!", nameof(nome));
            if (valor_total <= 0)
                throw new ArgumentException("O valor do parcelamento deve ser positivo!", nameof(valor_total));
            if (quantidadeParcelas <= 0)
                throw new ArgumentException("A quantidade de parcelas deve ser positiva!", nameof(quantidadeParcelas));
            if (dataInicio < DateTime.UtcNow.Date)
                throw new ArgumentException("A data de início não pode ser no passado!", nameof(dataInicio));
            Nome = nome;
            Valor_Total = valor_total;
            QuantidadeParcelas = quantidadeParcelas;
            DataInicio = dataInicio;
            AtualizarDataAtualizacao();
        }
        protected Parcelamento()
        {
        }
    }
}