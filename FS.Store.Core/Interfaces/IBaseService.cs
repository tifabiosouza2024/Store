using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IBaseService<Entity, Id> where Entity : BaseEntity<Id>
    {
    }
}
