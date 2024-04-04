// using Interseguro.ADMWR.Backend.Persistence.Contexts;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
// using System.Threading.Tasks;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        protected readonly RifamosContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(RifamosContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> Get(dynamic id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Post(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public async Task PostList(List<TEntity> lstEntity)
        {
            _dbSet.AddRange(lstEntity);
            await SaveChanges();
        }

        public async Task Put(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public async Task PutList(List<TEntity> lstEntity)
        {
            _dbSet.UpdateRange(lstEntity);
            await SaveChanges();
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}