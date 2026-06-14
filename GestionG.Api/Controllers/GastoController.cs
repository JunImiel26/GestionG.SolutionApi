using GestionG.Application.DTOs.Gasto;
using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; 
using GestionG.Application.Helpers;

namespace GestionG.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GastoController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastoController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Usuario,Contador")]
        public async Task<IActionResult> GetById(int id)
        {
            var gasto = await _gastoService.ObterPorId(id);
            if (gasto == null) return NotFound("Gasto no encontrado.");
            return Ok(gasto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gastoService.ObtenerTodosAsync());
        }

        [HttpGet("usuario/{idUsuario}")]
        [Authorize(Roles = "Admin,Contador,Usuario")]
        public async Task<IActionResult> GetByUser(int idUsuario)
        {
            // Si es un usuario normal, sólo puede consultar sus propios gastos
            var roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value);
            var isUsuario = roles.Contains("Usuario");
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (isUsuario && userIdClaim != null)
            {
                if (!int.TryParse(userIdClaim.Value, out var userId) || userId != idUsuario)
                    return Forbid();
            }
            return Ok(await _gastoService.ObtenerPorUsuarioAsync(idUsuario));
        }

        [HttpGet("usuario/{idUsuario}/pdf")]
        [Authorize(Roles = "Contador")]
        public async Task<IActionResult> DownloadPdfByUser(int idUsuario)
        {
            var gastos = await _gastoService.ObtenerPorUsuarioAsync(idUsuario);
            var total = await _gastoService.ObtenerTotalPorUsuarioAsync(idUsuario);

            // Generar PDF simple (bytes)
            var pdfBytes = PdfHelper.GenerateGastosPdf(gastos, total);

            return File(pdfBytes, "application/pdf", $"gastos_usuario_{idUsuario}.pdf");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] GastoCrearDTo dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value);
            var isUsuario = roles.Contains("Usuario");
            var isAdmin = roles.Contains("Admin");

            if (isUsuario)
            {
                // Usuario solo puede crear gastos para si mismo
                dto.IdUsuario = int.Parse(userIdClaim.Value);
            }
            else if (isAdmin)
            {
                // Admin puede crear gastos para cualquier usuario, pero requiere que IdUsuario venga en el body
                if (!dto.IdUsuario.HasValue)
                    return BadRequest("IdUsuario es requerido en el body cuando quien crea no es Usuario.");
            }
            else
            {
                // Otros roles no permitidos
                return Forbid();
            }

            var resultado = await _gastoService.CrearAsync(dto);
            return Ok(resultado);
        }
        

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gastoService.EliminarAsync(id);
            return NoContent();
        }
    }
}