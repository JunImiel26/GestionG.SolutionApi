using AutoMapper;
using GestionG.Application.DTOs.Categoria;
using GestionG.Application.Interface.Repository;
using GestionG.Application.Interface.Service;
using GestionG.Domain.Entities;

namespace GestionG.Application.Services
{
    public class Categoriaservice : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public Categoriaservice(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // 1. ObterPorId (Respetando el nombre sin 'n')
        public async Task<CategoriaDTo?> ObterPorId(int id)
        {
            var entidad = await _repository.ObtenerPorIdAsync(id);
            return _mapper.Map<CategoriaDTo>(entidad);
        }

        // 2. ObtenerTodasAsync
        public async Task<IEnumerable<CategoriaDTo>> ObtenerTodasAsync()
        {
            var entidades = await _repository.ObtenerTodasAsync();
            return _mapper.Map<IEnumerable<CategoriaDTo>>(entidades);
        }

        // 3. BuscarCategoriaAsync
        public async Task<IEnumerable<CategoriaDTo>> BuscarCategoriaAsync(string nombre)
        {
            var todas = await _repository.ObtenerTodasAsync();
            var filtradas = todas.Where(c => c.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<CategoriaDTo>>(filtradas);
        }

        // 4. CrearAsync
        public async Task<CategoriaDTo> CrearAsync(CategoriaCrearDTo dto)
        {
            var entidad = _mapper.Map<Categoria>(dto);

            await _repository.InsertarAsync(entidad);
            // ^ En este punto, EF ya llenó 'entidad.IdCat' con el valor de Postgres.

            // Debes mapear la ENTIDAD (que ya tiene el ID) de vuelta al DTO
            return _mapper.Map<CategoriaDTo>(entidad);
        }

        // 5. ActualizarAsync 
        public async Task<CategoriaDTo> ActualizarAsync(int id, CategoriaActualizarDTo dto)
        {
            var entidad = await _repository.ObtenerPorIdAsync(id);
            if (entidad == null) throw new Exception("Categoría no encontrada");

            _mapper.Map(dto, entidad);
            await _repository.ActualizarAsync(entidad);

            return _mapper.Map<CategoriaDTo>(entidad);
        }

        // 6. EliminarAsync (Debe devolver Task)
        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }

        
    }
}