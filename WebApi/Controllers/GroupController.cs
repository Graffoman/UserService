using AutoMapper;
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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(_mapper.Map<GroupModel>(await _service.GetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingGroupModel groupModel)
        {
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingGroupDto>(groupModel)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingGroupModel groupModel)
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingGroupModel, UpdatingGroupDto>(groupModel));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(_mapper.Map<List<GroupModel>>(await _service.GetListAsync()));
        }

        [HttpPost("userlist")]
        public async Task<IActionResult> GetUserListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetUserListAsync(id)));
        }
    }
}
