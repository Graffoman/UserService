using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Role;
using WebApi.Models.Role;
using WebApi.Models.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService service, ILogger<RoleController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(Guid id)
        {

            var role = await _service.GetByIdAsync(id);
            if (role == null)
                return NotFound();            
            return Ok(_mapper.Map<RoleModel>(role));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(CreatingRoleModel roleModel)
        {
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingRoleDto>(roleModel)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingRoleModel roleModel)
        {
            if (await _service.GetByIdAsync(id) == null)
                return NotFound();
            await _service.UpdateAsync(id, _mapper.Map<UpdatingRoleModel, UpdatingRoleDto>(roleModel));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _service.GetByIdAsync(id) == null)
                return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(_mapper.Map<List<RoleModel>>(await _service.GetListAsync()));
        }

        [HttpGet("userlist")]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetUserListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetUserListAsync(id)));
        }

        [HttpGet("usernotinrolelist")]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetUserNotInRoleListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetUserNotInRoleListAsync(id)));
        }
    }
}
