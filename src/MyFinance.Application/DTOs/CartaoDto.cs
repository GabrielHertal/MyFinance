namespace MyFinance.Application.DTOs
{
    public sealed record CartaoDto(Guid Id
                                 , string Nome
                                 , string Banco
                                 , string NumeroFinal
                                 , Guid UsuarioId
                                 , bool Ativo
                                 , decimal Limite);
}