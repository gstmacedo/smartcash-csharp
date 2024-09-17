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
    public class AssinaturaController : ControllerBase
    {
        private readonly AssinaturaRepository _assinaturaRepository;

        public AssinaturaController(AssinaturaRepository assinaturaRepository)
        {
            _assinaturaRepository = assinaturaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssinaturas()
        {
            var assinaturas = await _assinaturaRepository.GetAssinaturas();
            return Ok(assinaturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assinatura>> GetAssinatura(int id)
        {
            try
            {
                var assinatura = await _assinaturaRepository.GetAssinatura(id);
                if (assinatura == null) return NotFound();
                return assinatura;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter assinatura");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Assinatura>> AddAssinatura([FromBody] Assinatura assinatura)
        {
            try
            {
                if (assinatura == null) return BadRequest();
                var createdAssinatura = await _assinaturaRepository.AddAssinatura(assinatura);
                return CreatedAtAction(nameof(GetAssinatura), new { id = createdAssinatura.IdAssinatura }, createdAssinatura);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar assinatura");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Assinatura>> UpdateAssinatura(int id, Assinatura assinatura)
        {
            try
            {
                var existingAssinatura = await _assinaturaRepository.GetAssinatura(id);
                if (existingAssinatura == null) return NotFound($"Assinatura com id {id} não encontrada");

                assinatura.IdAssinatura = id;
                return await _assinaturaRepository.UpdateAssinatura(assinatura);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar assinatura");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssinatura(int id)
        {
            try
            {
                var assinaturaToDelete = await _assinaturaRepository.GetAssinatura(id);
                if (assinaturaToDelete == null) return NotFound($"Assinatura com id {id} não encontrada");

                await _assinaturaRepository.DeleteAssinatura(id);
                return Ok($"Assinatura com id {id} deletada");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar assinatura");
            }
        }
    }
}
