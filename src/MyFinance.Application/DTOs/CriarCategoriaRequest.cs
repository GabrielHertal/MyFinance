namespace MyFinance.Application.DTOs
{
    public sealed record CriarCategoriaRequest(string nome
                                              ,string descricao
                                              ,Guid UsuarioId
                                              ,bool Ativo);
}