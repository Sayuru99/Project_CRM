using Microsoft.EntityFrameworkCore;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Interfaces;
using MyApp.SharedDomain.ValueObjects;
using System.Net;

namespace MyApp.SharedDomain.Repositories
{
    public class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<PaginationResponse<TEntity>> GetAllAsync(Pagination pagination)
        {
            var results = await _dbSet.Skip(pagination.Skip).Take(pagination.Size).ToListAsync();
            return new PaginationResponse<TEntity>(results, pagination);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Task.Run(() => _dbSet.Update(entity));
        }

        public virtual Task Delete(TEntity entity)
        {
            return Task.Run(() => _dbSet.Remove(entity));
        }

        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new ExceptionBase(ex.InnerException?.Message ?? ex.Message, HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
