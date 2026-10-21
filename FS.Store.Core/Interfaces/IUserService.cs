using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IUserService : IBaseService<User, Guid>
    {
        Task<User?> FindByEmail(string email);
    }
}
