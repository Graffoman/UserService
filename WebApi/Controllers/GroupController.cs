using AutoMapper;
using MassTransit.Futures.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Group;
using WebApi.Models.Group;
using WebApi.Models.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService service, ILogger<GroupController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GroupModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var group = await _service.GetByIdAsync(id);
            if (group == null)
                return NotFound();
            return Ok(_mapper.Map<GroupModel>(group));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(CreatingGroupModel groupModel)
        {            
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingGroupDto>(groupModel)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingGroupModel groupModel)
        {
            if (await _service.GetByIdAsync(id) == null)
                return NotFound();
            await _service.UpdateAsync(id, _mapper.Map<UpdatingGroupModel, UpdatingGroupDto>(groupModel));
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

        [HttpPost("list")]
        [ProducesResponseType(typeof(IEnumerable<GroupModel>), 200)]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(_mapper.Map<List<GroupModel>>(await _service.GetListAsync()));
        }

        [HttpPost("userlist")]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetUserListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetUserListAsync(id)));
        }


        [HttpPost("usernotingrouplist")]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetUserNotInRoleListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetUserNotInGroupListAsync(id)));
        }
    }
}
