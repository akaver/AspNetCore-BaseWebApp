using System;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        // get standard repository for type TEntity
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;

        TRepository GetCustomRepository<TRepository>() where TRepository : class;
    }
}
