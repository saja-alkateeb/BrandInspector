using Microsoft.AspNetCore.Mvc;
using BrandInspector.Api.Models;
using BrandInspector.Api.Services;

namespace BrandInspector.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            // Simple authentication for testing purposes
            if (request.Username == "admin" && request.Password == "1234")
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new AuthResponse { Access = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
