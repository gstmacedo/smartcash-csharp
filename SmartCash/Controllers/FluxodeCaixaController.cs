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
    public class FluxoCaixaController : ControllerBase
    {
        private readonly FluxoCaixaRepository _fluxoCaixaRepository;

        public FluxoCaixaController(FluxoCaixaRepository fluxoCaixaRepository)
        {
            _fluxoCaixaRepository = fluxoCaixaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFluxoCaixas()
        {
            var fluxoCaixas = await _fluxoCaixaRepository.GetFluxoCaixas();
            return Ok(fluxoCaixas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FluxoCaixa>> GetFluxoCaixa(int id)
        {
            try
            {
                var fluxoCaixa = await _fluxoCaixaRepository.GetFluxoCaixa(id);
                if (fluxoCaixa == null) return NotFound();
                return fluxoCaixa;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter fluxo de caixa");
            }
        }

        [HttpPost]
        public async Task<ActionResult<FluxoCaixa>> AddFluxoCaixa([FromBody] FluxoCaixa fluxoCaixa)
        {
            try
            {
                if (fluxoCaixa == null) return BadRequest();
                var createdFluxoCaixa = await _fluxoCaixaRepository.AddFluxoCaixa(fluxoCaixa);
                return CreatedAtAction(nameof(GetFluxoCaixa), new { id = createdFluxoCaixa.IdFluxo }, createdFluxoCaixa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar fluxo de caixa");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FluxoCaixa>> UpdateFluxoCaixa(int id, FluxoCaixa fluxoCaixa)
        {
            try
            {
                var existingFluxoCaixa = await _fluxoCaixaRepository.GetFluxoCaixa(id);
                if (existingFluxoCaixa == null) return NotFound($"Fluxo de caixa com id {id} não encontrado");

                fluxoCaixa.IdFluxo = id;
                return await _fluxoCaixaRepository.UpdateFluxoCaixa(fluxoCaixa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar fluxo de caixa");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFluxoCaixa(int id)
        {
            try
            {
                var fluxoCaixaToDelete = await _fluxoCaixaRepository.GetFluxoCaixa(id);
                if (fluxoCaixaToDelete == null) return NotFound($"Fluxo de caixa com id {id} não encontrado");

                await _fluxoCaixaRepository.DeleteFluxoCaixa(id);
                return Ok($"Fluxo de caixa com id {id} deletado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar fluxo de caixa");
            }
        }
    }
}
