using FS.Store.Model.Entity;

namespace FS.Store.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        Task<User?> FindByEmail(string email);
    }
}
