using MyFinance.Domain.Enums;

namespace MyFinance.Application.DTOs
{
    public sealed record CriarContaRequest(string nome
                                          ,decimal saldoInicial 
                                          ,Guid UsuarioId
                                          ,TipoConta tipo
                                          ,bool ativo);
}