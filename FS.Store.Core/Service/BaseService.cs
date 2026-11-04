using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FS.Store.Core.Service
{
    public class BaseService<Entity, Id>(IBaseRepository<Entity, Id> repository) : IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
        public async Task<Entity?> AddOrUpdateAsync(Entity e, bool changePassword = false)
            => await repository.AddOrUpdateAsync(e, changePassword);

        public async Task<int> CountAsync(Expression<Func<Entity, bool>>? filter = null)
            => await repository.CountAsync(filter);

        public async Task<bool> ExistsAsync(Expression<Func<Entity, bool>> filter)
            => await repository.ExistsAsync(filter);
        public async Task<Entity?> FindByIdAsync(Id id
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null)
            => await repository.FindByIdAsync(id, includeProperties: includeProperties);

        public async Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> filter
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null
            , bool asNoTracking = false)
            => await repository.FirstOrDefaultAsync(filter: filter, includeProperties: includeProperties, asNoTracking: asNoTracking);

        public Task<IEnumerable<Entity>?> GetAllAsync(Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null
            , bool asNoTracking = false)
            => repository.GetAllAsync(filter: filter, includeProperties: includeProperties, orderBy: orderBy, dynamicSorts: dynamicSorts, asNoTracking: asNoTracking);

        public Task<Pagination<Entity>> GetAllPagedAsync(int? currentPage, int? qttPerPage
            , Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IIncludableQueryable<Entity, object?>>? includeProperties = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null
            , bool asNoTracking = false)
            => repository.GetAllPagedAsync(currentPage: currentPage, qttPerPage: qttPerPage, filter: filter, includeProperties: includeProperties, orderBy: orderBy, dynamicSorts: dynamicSorts, asNoTracking: asNoTracking);

        public Task<long> LongCountAsync(Expression<Func<Entity, bool>>? filter = null)
            => repository.LongCountAsync(filter);

        public Task SaveChangesAsync()
            => repository.SaveChangesAsync();
    }
}
