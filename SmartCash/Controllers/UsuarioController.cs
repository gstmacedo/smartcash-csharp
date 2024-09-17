using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCash.Models;
using SmartCash.Repository;
using System.Threading.Tasks;
using System;

namespace SmartCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuario(id);
                if (usuario == null) return NotFound();
                return usuario;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter usuário");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> AddUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null) return BadRequest();
                var createdUsuario = await _usuarioRepository.AddUsuario(usuario);
                return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.IdUsuario }, createdUsuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar usuário");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> UpdateUsuario(int id, Usuario usuario)
        {
            try
            {
                var existingUsuario = await _usuarioRepository.GetUsuario(id);
                if (existingUsuario == null) return NotFound($"Usuário com id {id} não encontrado");

                usuario.IdUsuario = id;
                return await _usuarioRepository.UpdateUsuario(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar usuário");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuarioToDelete = await _usuarioRepository.GetUsuario(id);
                if (usuarioToDelete == null) return NotFound($"Usuário com id {id} não encontrado");

                await _usuarioRepository.DeleteUsuario(id);
                return Ok($"Usuário com id {id} deletado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar usuário");
            }
        }
    }
}
