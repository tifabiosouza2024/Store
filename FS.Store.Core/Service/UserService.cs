using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Dtos;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace FS.Store.Core.Service
{
    public class UserService(IUserRepository repository
        , PasswordHelper passwordHelper
        , CurrentUserHelper currentUserHelper) : BaseService<User, Guid>(repository), IUserService
    {
        public async Task CreateNewUserOperationAsync(AssignUserToOperationDto userDto)
        {
            var user = await FindByEmailAsync(userDto.Email.Trim());

            user ??= new User
            {
                Email = userDto.Email.Trim(),
                PasswordHash = passwordHelper.HashPassword(null!, userDto.Password),
                CreatedAt = DateTime.Now,
                IsActive = true,
                MustChangePassword = true
            };

            await repository.AddOrUpdateAsync(user);

            var now = DateTime.Now;
            var userOperation = new OperationUserRole
            {
                OperationId = userDto.OperationId,
                RoleId = userDto.RoleId,
                IsActive = true,
                InvitedAt = now,
                Status = Model.Enums.RoleStatusEnum.Pending,
                InvitedByUserId = userDto.InvitedByUserId
            };
            if (user.Operations == null)
                user.Operations = [];

            user.Operations.Add(userOperation);

            await repository.SaveChangesAsync();
        }

        public async Task<User?> FindByEmailAsync(string email)
            => await FirstOrDefaultAsync(u => email.Equals(u.Email), includeProperties: q => q.Include(o => o.Operations));

        public async Task<Pagination<User>> GetAllPagedAsync(int? currentPage
            , int? qttPerPage
            , Func<IQueryable<User>, IQueryable<User>>? orderBy = null
            , Dictionary<string, string>? dynamicSorts = null)
        {
            var operationId = currentUserHelper.CurrentOperationId;

            var users = await base.GetAllPagedAsync(currentPage, qttPerPage
                , filter: u => u.Operations.Any(o => o.OperationId == operationId)
                , orderBy: orderBy
                , dynamicSorts: dynamicSorts);

            return users;
        }
    }
}
