using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class CashRegisterSessionRepository(AppDbContext dbcontext) : BaseRepository<CashRegisterSession, long>(dbcontext), ICashRegisterSessionRepository
    {
    }
}
