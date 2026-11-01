using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class CashClosingRepository(AppDbContext _dbContext) : BaseRepository<CashClosing, long>(_dbContext), ICashClosingRepository
    {
    }
}
