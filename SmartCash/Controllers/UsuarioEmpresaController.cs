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
    public class UsuarioEmpresaController : ControllerBase
    {
        private readonly UsuarioEmpresaRepository _usuarioEmpresaRepository;

        public UsuarioEmpresaController(UsuarioEmpresaRepository usuarioEmpresaRepository)
        {
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarioEmpresas()
        {
            try
            {
                var usuarioEmpresas = await _usuarioEmpresaRepository.GetUsuarioEmpresas();
                return Ok(usuarioEmpresas);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter usuário-empresa");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEmpresa>> GetUsuarioEmpresa(int id)
        {
            try
            {
                var usuarioEmpresa = await _usuarioEmpresaRepository.GetUsuarioEmpresa(id);
                if (usuarioEmpresa == null) return NotFound();
                return usuarioEmpresa;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter usuário-empresa");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioEmpresa>> AddUsuarioEmpresa([FromBody] UsuarioEmpresa usuarioEmpresa)
        {
            try
            {
                if (usuarioEmpresa == null) return BadRequest();
                var createdUsuarioEmpresa = await _usuarioEmpresaRepository.AddUsuarioEmpresa(usuarioEmpresa);
                return CreatedAtAction(nameof(GetUsuarioEmpresa), new { id = createdUsuarioEmpresa.IdUsuarioEmpresa }, createdUsuarioEmpresa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar usuário-empresa");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioEmpresa>> UpdateUsuarioEmpresa(int id, [FromBody] UsuarioEmpresa usuarioEmpresa)
        {
            try
            {
                var existingUsuarioEmpresa = await _usuarioEmpresaRepository.GetUsuarioEmpresa(id);
                if (existingUsuarioEmpresa == null) return NotFound($"Usuário-empresa com id {id} não encontrado");

                usuarioEmpresa.IdUsuarioEmpresa = id;
                return await _usuarioEmpresaRepository.UpdateUsuarioEmpresa(usuarioEmpresa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar usuário-empresa");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioEmpresa(int id)
        {
            try
            {
                var usuarioEmpresaToDelete = await _usuarioEmpresaRepository.GetUsuarioEmpresa(id);
                if (usuarioEmpresaToDelete == null) return NotFound($"Usuário-empresa com id {id} não encontrado");

                await _usuarioEmpresaRepository.DeleteUsuarioEmpresa(id);
                return Ok($"Usuário-empresa com id {id} deletado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar usuário-empresa");
            }
        }
    }
}
