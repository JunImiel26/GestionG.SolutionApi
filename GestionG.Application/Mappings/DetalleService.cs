using AutoMapper;
using GestionG.Application.DTOs.Detalle;
using GestionG.Application.Interface.Repository;
using GestionG.Application.Interface.Service;
using GestionG.Domain.Entities;

namespace GestionG.Application.Services
{
    public class DetalleService : IDetalleService
    {
        private readonly IDetalleRepository _repository;
        private readonly IGastoRepository _gastoRepository;
        private readonly IMapper _mapper;

        public DetalleService(IDetalleRepository repository, IGastoRepository gastoRepository, IMapper mapper)
        {
            _repository = repository;
            _gastoRepository = gastoRepository;
            _mapper = mapper;
        }

        public async Task<DetalleDTo?> ObterPorId(int id)
        {
            var entidad = await _repository.ObtenerPorIdAsync(id);
            return _mapper.Map<DetalleDTo>(entidad);
        }

        public async Task<IEnumerable<DetalleDTo>> ObtenerPorGastoAsync(int idGasto)
        {
            var entidades = await _repository.ObtenerPorGastoAsync(idGasto);
            return _mapper.Map<IEnumerable<DetalleDTo>>(entidades);
        }

        public async Task<DetalleDTo> CrearAsync(DetalleCrearDTo dto, int? currentUserId = null)
        {
            // 1. Mapeamos el DTO a la Entidad
            var detalle = _mapper.Map<Detalle>(dto);

            // 2. Forzamos que el IdGasto de la entidad sea el que viene en el DTO
            if (!dto.IdGasto.HasValue || dto.IdGasto.Value <= 0)
            {
                throw new InvalidOperationException("El IdGasto debe ser mayor a cero y existir en la base de datos.");
            }

            detalle.IdGasto = dto.IdGasto.Value;

            // Validar que el gasto exista
            var gasto = await _gastoRepository.ObtenerPorIdAsync(detalle.IdGasto);
            if (gasto == null) throw new InvalidOperationException("El gasto especificado no existe.");

            // Si se pasa currentUserId, validar pertenencia
            if (currentUserId.HasValue && gasto.IdUsuario != currentUserId.Value)
            {
                throw new UnauthorizedAccessException("No tienes permiso para agregar detalles a este gasto.");
            }

            // 3. Guardamos
            var entidadGuardada = await _repository.InsertarAsync(detalle);

            return _mapper.Map<DetalleDTo>(entidadGuardada);
        }

        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }
    }
}