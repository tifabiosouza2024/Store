using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using System.Linq.Expressions;

namespace FS.Store.Core.Service
{
    public class BaseService<Entity, Id>(IBaseRepository<Entity, Id> repository) : IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
        public async Task<Entity?> AddOrUpdateAsync(Entity e, bool changePassword = false)
            => await repository.AddOrUpdateAsync(e, changePassword);

        public async Task<int> CountAsync(Expression<Func<Entity, bool>>? filter = null)
            => await repository.CountAsync(filter);

        public async Task<Entity?> FindByIdAsync(Id id)
            => await repository.FindByIdAsync(id);

        public async Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> predicate)
            => await repository.FirstOrDefaultAsync(predicate);

        public Task<IEnumerable<Entity?>> GetAllAsync(Expression<Func<Entity, bool>> filter
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null)
            => repository.GetAllAsync(filter, orderBy, dynamicSorts);

        public Task<Pagination<Entity>> GetAllPagedAsync(int? currentPage, int? qttPerPage
            , Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null)
            => repository.GetAllPagedAsync(currentPage, qttPerPage, filter, orderBy, dynamicSorts);

        public Task<long> LongCountAsync(Expression<Func<Entity, bool>>? filter = null)
            => repository.LongCountAsync(filter);

        public Task SaveChangesAsync()
            => repository.SaveChangesAsync();
    }
}
