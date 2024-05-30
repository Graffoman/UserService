using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.UserGroup;
using WebApi.Models.UserGroup;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGroupController> _logger;

        public UserGroupController(IUserGroupService service, ILogger<UserGroupController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("addUserToGroupAsync")]
        public async Task<IActionResult> AddUserToGroupAsync(CreatingUserGroupModel userGroupModel)
        {
            return Ok(await _service.AddUserToGroupAsync(_mapper.Map<CreatingUserGroupDto>(userGroupModel)));
        }

        [HttpPost("deleteUserFromGroupAsync")]
        public async Task<IActionResult> DeleteUserFromGroupAsync(CreatingUserGroupModel userGroupModel)
        {
            await _service.DeleteUserFromGroupAsync(_mapper.Map<CreatingUserGroupDto>(userGroupModel));
            return Ok();
        }
    }
}
