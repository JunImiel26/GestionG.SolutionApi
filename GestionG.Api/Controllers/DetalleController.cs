using GestionG.Application.DTOs.Detalle;
using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionG.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DetalleController : ControllerBase
    {
        private readonly IDetalleService _detalleService;

        public DetalleController(IDetalleService detalleService)
        {
            _detalleService = detalleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detalle = await _detalleService.ObterPorId(id);
            if (detalle == null) return NotFound("Detalle no encontrado.");
            return Ok(detalle);
        }

     
        [HttpGet("gasto/{idGasto}")]
        public async Task<IActionResult> GetByGasto(int idGasto)
        {
            var detalles = await _detalleService.ObtenerPorGastoAsync(idGasto);
            return Ok(detalles);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] DetalleCrearDTo dto)
        {
            // Si el usuario es rol Usuario, validar que el gasto pertenezca al mismo usuario
            var roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value);
            var isUsuario = roles.Contains("Usuario");
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int? currentUserId = null;
            if (isUsuario && userIdClaim != null)
            {
                currentUserId = int.Parse(userIdClaim.Value);
            }

            var resultado = await _detalleService.CrearAsync(dto, currentUserId);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _detalleService.EliminarAsync(id);
            return NoContent();
        }
    }
}