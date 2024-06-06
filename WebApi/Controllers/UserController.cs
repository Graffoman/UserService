using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts.User;
using WebApi.Models.User;
using WebApi.Models.Group;
using WebApi.Models.Role;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(_mapper.Map<UserModel>(await _service.GetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingUserModel userModel)
        {
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingUserDto>(userModel)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingUserModel userModel)
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingUserModel, UpdatingUserDto>(userModel));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("filteredlist")]
        public async Task<IActionResult> GetPagedAsync(UserFilterModel filterModel)
        {
            var filterDto = _mapper.Map<UserFilterModel, UserFilterDto>(filterModel);
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetPagedAsync(filterDto)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetAsyncByEmailPassword(UserLoginModel userLoginModel)
        {

            return Ok(_mapper.Map<UserModel>(await _service.Login(_mapper.Map<UserLoginModel, UserLoginDto>(userLoginModel))));
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetListAsync()
        {            
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetListAsync()));
        }

        [HttpPost("grouplist")]
        public async Task<IActionResult> GetGroupListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<GroupModel>>(await _service.GetGroupListAsync(id)));
        }

        [HttpPost("rolelist")]
        public async Task<IActionResult> GetRoleListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<RoleModel>>(await _service.GetRoleListAsync(id)));
        }
    }
}
