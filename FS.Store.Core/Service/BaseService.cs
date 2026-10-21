using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class BaseService<Entity, Id>(IBaseRepository<Entity, Id> _repository) : IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
    }
}
