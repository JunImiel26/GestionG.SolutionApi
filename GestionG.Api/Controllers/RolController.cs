using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionG.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

     
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var roles = await _rolService.ObtenerTodosAsync();
            return Ok(roles);
        }

       
    
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensaje = "Id inválido" });

            var rol = await _rolService.ObtenerPorIdAsync(id);

            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            return Ok(rol);
        }

       
       
       
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { mensaje = "Nombre inválido" });

            var rol = await _rolService.ObtenerPorNombreAsync(nombre);

            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            return Ok(rol);
        }

        
      
       
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] string nombreRol)
        {
            if (string.IsNullOrWhiteSpace(nombreRol))
                return BadRequest(new { mensaje = "El nombre del rol es obligatorio" });

            var creado = await _rolService.CrearAsync(nombreRol);

            if (!creado)
                return BadRequest(new { mensaje = "No se pudo crear el rol" });

            return Ok(new { mensaje = "Rol creado correctamente" });
        }

        
        
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] string nuevoNombre)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(nuevoNombre))
                return BadRequest(new { mensaje = "Datos inválidos" });

            var actualizado = await _rolService.ActualizarAsync(id, nuevoNombre);

            if (!actualizado)
                return NotFound(new { mensaje = "Rol no encontrado o no actualizado" });

            return NoContent();
        }

        
        
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensaje = "Id inválido" });

            var eliminado = await _rolService.EliminarAsync(id);

            if (!eliminado)
                return NotFound(new { mensaje = "Rol no encontrado" });

            return NoContent();
        }
    }

}

