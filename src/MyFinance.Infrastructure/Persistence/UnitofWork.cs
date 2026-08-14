using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Infrastructure.Persistence.Context;

namespace MyFinance.Infrastructure.Persistence
{
    public class UnitofWork : IUnitofWork
    {
        private readonly MyFinanceDbContext _dbContext;
        public UnitofWork(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _dbContext.SaveChangesAsync(cancellationToken);
    }
}