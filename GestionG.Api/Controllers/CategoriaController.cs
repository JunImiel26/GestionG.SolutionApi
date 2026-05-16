
using GestionG.Application.DTOs.Categoria;
using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace GestionG.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoriaDTo>>> Obtenertodos()
        {
            var categorias = await _categoriaService.ObtenerTodasAsync();
            if (categorias == null || !categorias.Any())
                return NotFound("No hay categorias registradas");

            return Ok(categorias);
        }


        [HttpGet("{id:int}", Name = "ObtenerPorId")]
        public async Task<ActionResult<CategoriaDTo>> ObtenerPorId(int id)
        {
            var categoria = await _categoriaService.ObterPorId(id);
            return Ok(categoria);
        }
  
        [HttpPost]
        public async Task<ActionResult<CategoriaDTo>> Crear([FromBody] CategoriaCrearDTo dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var nuevaCategoria = await _categoriaService.CrearAsync(dto);
            return CreatedAtAction("ObtenerPorId", new { id = nuevaCategoria.IdCat }, nuevaCategoria);
        }
      
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTo>> Actualizar(int id, [FromBody] CategoriaActualizarDTo dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _categoriaService.ActualizarAsync(id, dto);
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _categoriaService.EliminarAsync(id);
            return NoContent();
        }
    }
}
