using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class CashMovementRepository(AppDbContext _dbContext) : BaseRepository<CashMovement, long>(_dbContext), ICashMovementRepository
    {
    }
}
