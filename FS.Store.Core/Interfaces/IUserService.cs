using FS.Store.Model.Dtos;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IUserService : IBaseService<User, Guid>
    {
        Task CreateNewUserOperationAsync(AssignUserToOperationDto userDto);
        Task<User?> FindByEmailAsync(string email);

        Task<Pagination<User>> GetAllPagedAsync(int? currentPage, int? qttPerPage
            , Func<IQueryable<User>, IQueryable<User>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null);
    }
}
