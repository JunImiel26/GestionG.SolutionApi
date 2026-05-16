using GestionG.Application.DTOs.Usuario; 
using GestionG.Application.Interface.Service;
using GestionG.Application.Mappings;
using GestionG.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionG.Api.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<UsuarioDTo>>> ObtenerTodos([FromQuery] int pagina = 1, [FromQuery] int tamano = 10)
        {

            var registros = await _service.ObtenerUsuariosAsync(pagina, tamano);
            var total = await _service.ContarAsync();

            return Ok(new RespuestaPaginada<UsuarioDTo>(registros, total, pagina, tamano));
        }

        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        
        public async Task<ActionResult<UsuarioDTo>> ObtenerUsuario(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);
            return Ok(registro);
        }

    }
}