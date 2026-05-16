using GestionG.Application.DTOs.Gasto;
using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; 

namespace GestionG.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    
    public class GastoController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastoController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gasto = await _gastoService.ObterPorId(id);
            if (gasto == null) return NotFound("Gasto no encontrado.");
            return Ok(gasto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gastoService.ObtenerTodosAsync());
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUser(int idUsuario)
        {
            return Ok(await _gastoService.ObtenerPorUsuarioAsync(idUsuario));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GastoCrearDTo dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            dto.IdUsuario = int.Parse(userIdClaim.Value);

            
            var resultado = await _gastoService.CrearAsync(dto);
            return Ok(resultado);
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gastoService.EliminarAsync(id);
            return NoContent();
        }
    }
}