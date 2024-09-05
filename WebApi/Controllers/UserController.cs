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
using Domain.Entities;
using Services.Contracts.Role;
using Newtonsoft.Json;
using RabbitMQ.Abstractions;
using System.Net;

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
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserModel>(user));           
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(CreatingUserModel userModel)
        {
            var userid = await _service.CreateAsync(_mapper.Map<CreatingUserDto>(userModel));
            if (userid == Guid.Empty)
                return BadRequest();
            return Ok(userid);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingUserModel userModel)
        {
            if (await _service.GetByIdAsync(id) == null)
                return NotFound();
            await _service.UpdateAsync(id, _mapper.Map<UpdatingUserModel, UpdatingUserDto>(userModel));
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
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> GetListAsync()
        {            
            return Ok(_mapper.Map<List<UserModel>>(await _service.GetListAsync()));
        }

        [HttpGet("grouplist")]
        [ProducesResponseType(typeof(IEnumerable<GroupModel>), 200)]
        public async Task<IActionResult> GetGroupListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<GroupModel>>(await _service.GetGroupListAsync(id)));
        }

        [HttpGet("rolelist")]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), 200)]
        public async Task<IActionResult> GetRoleListAsync(Guid id)
        {
            return Ok(_mapper.Map<List<RoleModel>>(await _service.GetRoleListAsync(id)));
        }
    }
}
