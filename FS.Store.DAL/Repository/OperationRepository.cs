using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class OperationRepository(AppDbContext _dbContext) : BaseRepository<Operation, Guid>(_dbContext), IOperationRepository
    {
    }
}
