using MyFinance.Domain.Enums;

namespace MyFinance.Application.DTOs
{
    public sealed record ContaDto(Guid Id
                                 ,string Nome
                                 ,decimal Saldo
                                 ,Guid UsuarioId
                                 ,TipoConta Tipo
                                 ,bool Ativo);
}