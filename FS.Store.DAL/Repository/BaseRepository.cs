using FS.Store.DAL.Context;
using FS.Store.DAL.Exceptions;
using FS.Store.DAL.Extensions;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FS.Store.DAL.Repository
{
    public class BaseRepository<Entity, Id>(AppDbContext _context) : IBaseRepository<Entity, Id> where Entity : BaseEntity<Id>
    {
        private readonly DbSet<Entity> entities = _context.Set<Entity>();
        private readonly AppDbContext context = _context;

        #region Queryable
        private IQueryable<Entity> Queryable(Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null)
        {
            var query = entities.AsQueryable().Where(e => !e.Deleted);
            if (filter is not null)
                query = query.Where(filter);

            if (dynamicSorts is not null && dynamicSorts.Count > 0)
                query.ApplySortingDynamic(dynamicSorts);
            else if (orderBy is not null)
                query = orderBy(query);

            return query;

        }
        #endregion

        public async Task<Entity?> AddOrUpdateAsync(Entity e, bool changePassword = false)
        {
            if (IsNewId(e.Id))
                await entities.AddAsync(e);
            else
            {
                var ef = await FindByIdAsync(e.Id);
                if (ef is not null)
                {
                    entities.Entry(ef).CurrentValues.SetValues(e);
                    if (e is User && !changePassword)
                        entities.Entry(ef).Property(nameof(User.PasswordHash)).IsModified = false;
                }
                else
                    throw new RepositoryException($"The Entity [{e.GetType().Name}] contains ID and is invalid");
            }

            await SaveChangesAsync();

            return e;
        }

        public async Task<int> CountAsync(Expression<Func<Entity, bool>>? filter = null)
            => await Queryable(filter).CountAsync();

        public async Task<bool> ExistsAsync(Expression<Func<Entity, bool>> filter)
            => await Queryable(filter).AnyAsync();

        public async Task<Entity?> FindByIdAsync(Id id)
            => await FirstOrDefaultAsync(u => id!.Equals(u.Id));

        public async Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> filter)
            => await Queryable(filter).FirstOrDefaultAsync();

        public async Task<IEnumerable<Entity?>> GetAllAsync(Expression<Func<Entity, bool>> filter
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null)
            => await Queryable(filter: filter, dynamicSorts: dynamicSorts).ToListAsync();

        public async Task<Pagination<Entity>> GetAllPagedAsync(int? currentPage, int? qttPerPage
            , Expression<Func<Entity, bool>>? filter = null
            , Func<IQueryable<Entity>, IQueryable<Entity>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null)
        {
            if (qttPerPage is null || qttPerPage <= 0)
                qttPerPage = 10;

            if (currentPage is null)
                currentPage = 0;

            var qtt = await CountAsync(filter);

            var pagination = new Pagination<Entity>() { QttPerPage = qttPerPage.GetValueOrDefault() };
            pagination.CurrentPage = currentPage.GetValueOrDefault();
            pagination.TotalItems = qtt;
            pagination.Data = await Queryable(filter: filter, dynamicSorts: dynamicSorts).ToListAsync();

            return pagination;
        }

        public async Task<long> LongCountAsync(Expression<Func<Entity, bool>>? filter = null)
            => await Queryable(filter).LongCountAsync();


        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();

        #region Auxiliares
        private static bool IsNewId(Id id)
        {
            if (id is null)
                return true;

            if (id is long)
                return Convert.ToInt64(id) == 0;

            if (id is Guid)
                return Guid.Empty.Equals(id);

            if (id is int)
                return Convert.ToInt32(id) == 0;

            if (id is string)
                return string.IsNullOrEmpty(id.ToString());

            throw new RepositoryException("Tipo de Id não identificado");
        }
        #endregion

    }
}
