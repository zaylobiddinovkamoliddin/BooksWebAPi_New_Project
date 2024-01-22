using BooksWepAPiDtos.UserDto;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebAPi_New_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto);
            if (result.IsSuccessed)
            {
                return Ok("User Created");
            } 
            return BadRequest(result.ErrorrMessages); 
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _userService.LoginUserAsync(loginUserDto);
            if (result.IsSuccessed)
            {
                return Ok(result.Token);
            }
            return BadRequest(result.ErrorMessages);
        }
    }
}
