using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class OperationUserRoleRepository(AppDbContext _dbContext) : BaseRepository<OperationUserRole, long>(_dbContext), IOperationUserRoleRepository
    {
    }
}
