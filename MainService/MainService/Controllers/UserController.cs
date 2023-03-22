using System.Security.Claims;
using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.UserOutput;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL;
using DAL.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace MainService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;


        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUser user, CancellationToken token)
        {
            try
            {
                await _userService.CreateAsync(user, token);

                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
