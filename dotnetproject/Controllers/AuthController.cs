using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models; // Replace with your actual namespace and models
using System.Threading.Tasks;
using dotnetproject.Services;

namespace dotnetproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        // Add other services as needed, e.g., a user service for validating credentials

        public AuthController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Validate credentials (This should be done with a user service)
            // For demonstration purposes, we'll use a simple check
            if (loginModel.Username == "admin" && loginModel.Password == "password")
            {
                var token = _jwtTokenService.GenerateToken(loginModel.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
