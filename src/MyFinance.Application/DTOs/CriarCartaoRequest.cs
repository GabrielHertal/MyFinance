namespace MyFinance.Application.DTOs
{
    public sealed record CriarCartaoRequest(string Nome
                                          , string Banco
                                          , string NumeroFinal
                                          , Guid UsuarioId
                                          , decimal Limite);
}