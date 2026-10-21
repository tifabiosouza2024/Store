using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class UserRepository(AppDbContext _dbContext) : BaseRepository<User, Guid>(_dbContext), IUserRepository
    {
    }
}
