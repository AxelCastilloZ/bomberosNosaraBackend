using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BomberosNosaraBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserCredential credentials)
        {
            try
            {
                var token = await _authService.LoginAsync(credentials);
                return Ok(token); 
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Credenciales inválidas");
            }
        }
    }
}
