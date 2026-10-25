using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos.API;
using FS.Store.Model.Dtos.Operations;
using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<OperationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<OperationDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOperationsByUser()
        {
            var userId = User.GetUserId();

            if (userId == Guid.Empty)
                return BadRequest();

            var user = await userService.FindByIdAsync(userId);
            var operations = user?.Operations?.ToDto<OperationDto>();

            if (operations == null)
                return NotFound(ApiResponseDto<OperationDto>.Fail("Usuario sem operacoes vinculadas"));

            return Ok(ApiResponseDto<OperationDto>.Ok(operations, "Operacoes Localizadas"));
        }
    }
}
