using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Role;
using WebApi.Models.Role;

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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(_mapper.Map<RoleModel>(await _service.GetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingRoleModel roleModel)
        {
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingRoleDto>(roleModel)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingRoleModel roleModel)
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingRoleModel, UpdatingRoleDto>(roleModel));
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
            return Ok(_mapper.Map<List<RoleModel>>(await _service.GetListAsync()));
        }
    }
}
