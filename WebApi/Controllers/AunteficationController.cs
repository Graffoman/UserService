using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.User;
using WebApi.Models.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AunteficationController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public AunteficationController(IUserService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetAsyncByEmailPassword(UserLoginModel userLoginModel)
        {

            return Ok(_mapper.Map<UserModel>(await _service.Login(_mapper.Map<UserLoginModel, UserLoginDto>(userLoginModel))));
        }
    }
}
