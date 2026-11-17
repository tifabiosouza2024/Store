using FS.Store.Core.Extension;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos.API;
using FS.Store.Model.Dtos.API.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.API.Controllers
{
    [Authorize]
    [Route("[area]/[controller]")]
    [ApiController]
    [Area("api")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("checkEmail")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SUPER,ADMIN,MANAGER")]
        public async Task<IActionResult> CheckEmail([FromBody] CheckEmailRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest();

            var user = await userService.FindByEmailAsync(request.Email.Trim());

            if (user != null && user.Operations != null && user.Operations.Any(o => o.OperationId == User.GetOperationId()))
            {
                return BadRequest(ApiResponseDto<CheckEmailResponseDto>.Fail($"Já existe usuário cadastrado com o e-mail {request.Email} em {User.GetOperationName()}", new() { Exists = true }));
            }

            return Ok(ApiResponseDto<CheckEmailResponseDto>.Ok(new() { Exists = user != null }));
        }
    }
}
