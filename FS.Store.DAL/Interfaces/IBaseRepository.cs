using FS.Store.Model.Entity;
using System.Linq.Expressions;

namespace FS.Store.DAL.Interfaces
{
    public interface IBaseRepository<Entity, Id> where Entity : BaseEntity<Id>
    {
        Task<Entity?> AddOrUpdateAsync(Entity e, bool changePassword = false);
        Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> predicate);
    }
}
