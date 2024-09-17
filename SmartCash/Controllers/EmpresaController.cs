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
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaRepository _empresaRepository;

        public EmpresaController(EmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _empresaRepository.GetEmpresas();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            try
            {
                var empresa = await _empresaRepository.GetEmpresa(id);
                if (empresa == null) return NotFound();
                return empresa;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter empresa");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> AddEmpresa([FromBody] Empresa empresa)
        {
            try
            {
                if (empresa == null) return BadRequest();
                var createdEmpresa = await _empresaRepository.AddEmpresa(empresa);
                return CreatedAtAction(nameof(GetEmpresa), new { id = createdEmpresa.IdEmpresa }, createdEmpresa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar empresa");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Empresa>> UpdateEmpresa(int id, Empresa empresa)
        {
            try
            {
                var existingEmpresa = await _empresaRepository.GetEmpresa(id);
                if (existingEmpresa == null) return NotFound($"Empresa com id {id} não encontrada");

                empresa.IdEmpresa = id;
                return await _empresaRepository.UpdateEmpresa(empresa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar empresa");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            try
            {
                var empresaToDelete = await _empresaRepository.GetEmpresa(id);
                if (empresaToDelete == null) return NotFound($"Empresa com id {id} não encontrada");

                await _empresaRepository.DeleteEmpresa(id);
                return Ok($"Empresa com id {id} deletada");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar empresa");
            }
        }
    }
}
