using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using System.Linq.Expressions;

namespace FS.Store.Core.Interfaces
{
    public interface IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
        Task<Entity?> AddOrUpdateAsync(Entity e, bool changePassword = false);

        Task<int> CountAsync(Expression<Func<Entity, bool>>? filter = null);

        Task<bool> ExistsAsync(Expression<Func<Entity, bool>> filter);

        Task<Entity?> FindByIdAsync(Id id);

        Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> filter);

        Task<IEnumerable<Entity?>> GetAllAsync(Expression<Func<Entity, bool>> filter
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null);

        Task<Pagination<Entity>> GetAllPagedAsync(int? currentPage, int? qttPerPage
            , Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null);

        Task<long> LongCountAsync(Expression<Func<Entity, bool>>? filter = null);

        Task SaveChangesAsync();
    }
}
