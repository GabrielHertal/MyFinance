using MyFinance.Domain.Enums;

namespace MyFinance.Application.DTOs
{
    public sealed record CriarTransacaoRequest(
        Guid ContaId,
        Guid CategoriaId,
        Guid UsuarioId,
        string Descricao,
        decimal Valor,
        TipoTransacao Tipo,
        DateTime DataTransacao,
        DateTime? DataPagamento = null,
        StatusTransacao Status = StatusTransacao.Pendente);
}
