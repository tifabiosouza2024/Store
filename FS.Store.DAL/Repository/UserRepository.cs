using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class UserRepository(AppDbContext _dbContext) : BaseRepository<User, Guid>(_dbContext), IUserRepository
    {
        public async Task<User?> FindByEmail(string email)
        {
            return await FirstOrDefaultAsync(u => email.Equals(u.Email));
        }
    }
}
