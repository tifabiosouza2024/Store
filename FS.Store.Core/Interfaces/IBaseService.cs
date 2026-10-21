using FS.Store.Model.Entity;
using System.Linq.Expressions;

namespace FS.Store.Core.Interfaces
{
    public interface IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
        Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> predicate);
    }
}
