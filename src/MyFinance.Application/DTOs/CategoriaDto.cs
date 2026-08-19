namespace MyFinance.Application.DTOs
{
    public sealed record CategoriaDto(Guid Id
                                     ,string nome
                                     ,string descricao
                                     ,Guid UsuarioId
                                     ,bool Ativo);
}