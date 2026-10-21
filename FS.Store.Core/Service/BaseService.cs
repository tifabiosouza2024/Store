using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;
using System.Linq.Expressions;

namespace FS.Store.Core.Service
{
    public class BaseService<Entity, Id>(IBaseRepository<Entity, Id> repository) : IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
        public async Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> predicate)
            => await repository.FirstOrDefaultAsync(predicate);
    }
}
