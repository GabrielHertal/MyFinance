namespace MyFinance.Application.Interfaces.Repositories
{
    public interface IUnitofWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}