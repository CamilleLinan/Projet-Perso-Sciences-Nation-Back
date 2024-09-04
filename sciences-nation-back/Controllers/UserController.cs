using Microsoft.AspNetCore.Mvc;
using sciences_nation_back.Services.Interfaces;
using sciences_nation_back.Models;

namespace sciences_nation_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User cannot be null.");
            }

            try
            {
                await _userService.CreateUserAsync(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id.ToString() }, newUser);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest("Login details cannot be null.");
            }

            var isValidUser = await _userService.VerifyPasswordAsync(login.Email, login.Password);

            if (!isValidUser)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var user = await _userService.GetUserByEmailAsync(login.Email);
            var token = _jwtService.GenerateToken(user.Id, user.Email);

            return Ok(new { user.Id, token });
        }
    }
}