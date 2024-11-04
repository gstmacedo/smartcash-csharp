using Microsoft.AspNetCore.Mvc;
using SmartCash.Services;
using System.Threading.Tasks;
using SmartCash.Models;


namespace SmartCash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var usuario = await _authService.Authenticate(request.Email, request.Senha);
            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            return Ok("Login bem-sucedido.");
        }
    }
}
