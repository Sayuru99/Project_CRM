using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyApp.SharedDomain.Interfaces
{
    public interface IEFRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetAsync(Guid id);
        Task<PaginationResponse<TEntity>> GetAllAsync(Pagination pagination);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task Delete(TEntity entity);
        Task SaveChangesAsync();
    }
}
