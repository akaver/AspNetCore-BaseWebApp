using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;

namespace DAL.Helpers
{
    public interface IRepositoryProvider
    {
        // get standard repository for type TEntity
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;

        // get custom repository, based on interface
        TRepository GetCustomRepository<TRepository>() where TRepository : class;
    }
}
