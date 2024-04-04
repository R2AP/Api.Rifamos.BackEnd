using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories
{

    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        // Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        // Task Post(TEntity entity);
        // Task PostList(List<TEntity> lstEntity);
        // Task Put(TEntity entity);
        // Task PutList(List<TEntity> lstEntity);
        // Task Delete(TEntity entity);
        // Task<int> SaveChanges();
    }
}