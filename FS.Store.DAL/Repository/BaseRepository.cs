using FS.Store.DAL.Context;
using FS.Store.DAL.Exceptions;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FS.Store.DAL.Repository
{
    public class BaseRepository<Entity, Id>(AppDbContext _context) : IBaseRepository<Entity, Id> where Entity : BaseEntity<Id>
    {
        private readonly DbSet<Entity> entities = _context.Set<Entity>();
        private readonly AppDbContext context = _context;

        public IQueryable<Entity> Queryable(Expression<Func<Entity, bool>>? predicate = null)
        {
            var query = entities.AsQueryable().Where(e => !e.Deleted);
            if (predicate is not null)
                query = query.Where(predicate);

            return query;

        }

        public async Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> predicate)
            => await Queryable(predicate).FirstOrDefaultAsync();

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
                        entities.Entry(ef).Property(nameof(User.Password)).IsModified = false;
                }
                else
                    throw new RepositoryException($"The Entity [{e.GetType().Name}] contains ID and is invalid");
            }

            await SaveChangesAsync();

            return e;
        }


        public async Task<Entity?> FindByIdAsync(Id id)
            => await entities.Where(u => id!.Equals(u.Id)).FirstOrDefaultAsync();

        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();

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
    }
}
