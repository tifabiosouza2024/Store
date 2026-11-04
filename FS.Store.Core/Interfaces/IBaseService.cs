using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FS.Store.Core.Interfaces
{
    public interface IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
        Task<Entity?> AddOrUpdateAsync(Entity e, bool changePassword = false);

        Task<int> CountAsync(Expression<Func<Entity, bool>>? filter = null);

        Task<bool> ExistsAsync(Expression<Func<Entity, bool>> filter);

        Task<Entity?> FindByIdAsync(Id id
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null);

        Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> filter
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null
            , bool asNoTracking = true);

        Task<IEnumerable<Entity>?> GetAllAsync(Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null
            , bool asNoTracking = false);

        Task<Pagination<Entity>> GetAllPagedAsync(int? currentPage, int? qttPerPage
            , Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null
            , bool asNoTracking = false);

        Task<long> LongCountAsync(Expression<Func<Entity, bool>>? filter = null);

        Task SaveChangesAsync();
    }
}
