using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.UserRole;
using WebApi.Models.UserRole;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(IUserRoleService service, ILogger<UserRoleController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("adduserrole")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddUserToRoleAsync(CreatingUserRoleModel userRoleModel)
        {
            return Ok(await _service.AddUserToRoleAsync(_mapper.Map<CreatingUserRoleDto>(userRoleModel)));
        }

        [HttpDelete("deleteuserrole")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUserFromRoleAsync(CreatingUserRoleModel userRoleModel)
        {
            await _service.DeleteUserFromRoleAsync(_mapper.Map<CreatingUserRoleDto>(userRoleModel));
            return Ok();
        }
    }
}
