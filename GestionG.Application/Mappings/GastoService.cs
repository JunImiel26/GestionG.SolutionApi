using AutoMapper;
using GestionG.Application.DTOs.Gasto;
using GestionG.Application.Interface.Repository;
using GestionG.Application.Interface.Service;
using GestionG.Domain.Entities;

namespace GestionG.Application.Services
{
    public class GastoService : IGastoService
    {
        private readonly IDetalleRepository _detalleRepository;
        private readonly IGastoRepository _repository;
        private readonly IMapper _mapper;

        public GastoService(IGastoRepository repository, IDetalleRepository detalleRepository, IMapper mapper)
        {
            _detalleRepository = detalleRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GastoDTo?> ObterPorId(int id)
        {
            var entidad = await _repository.ObtenerPorIdAsync(id);
            return _mapper.Map<GastoDTo>(entidad);
        }

        public async Task<IEnumerable<GastoDTo>> ObtenerTodosAsync()
        {
            var entidades = await _repository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<GastoDTo>>(entidades);
        }

        public async Task<IEnumerable<GastoDTo>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            var entidades = await _repository.ObtenerPorUsuarioAsync(idUsuario);
            return _mapper.Map<IEnumerable<GastoDTo>>(entidades);
        }

        public async Task<decimal> ObtenerTotalPorUsuarioAsync(int idUsuario)
        {
            var gastos = await _repository.ObtenerPorUsuarioAsync(idUsuario);
            return gastos.Sum(g => g.TotalGeneral);
        }

        public async Task<GastoDTo> CrearAsync(GastoCrearDTo dto)
        {
          
            if (!dto.IdUsuario.HasValue)
                throw new InvalidOperationException("IdUsuario es requerido. Si eres Usuario, el servidor debe establecerlo desde el token.");

            var nuevoGasto = new Gasto
            {
                TotalGeneral = dto.MontoTotal,
                IdUsuario = dto.IdUsuario.Value,
                Fecha = DateTime.UtcNow
            };

            // 2. Insertar el encabezado
            var gastoGuardado = await _repository.InsertarAsync(nuevoGasto);

            // 3. Insertar los detalles (si vienen sin IdGasto, se asignará el IdGasto nuevo)
            if (dto.Detalles != null && dto.Detalles.Any())
            {
                foreach (var d in dto.Detalles)
                {
                    var detalle = _mapper.Map<Detalle>(d);
                    detalle.IdGasto = gastoGuardado.IdGasto;
                    await _detalleRepository.InsertarAsync(detalle);
                }
            }

            // 4. Retornar el DTO mapeado
            return _mapper.Map<GastoDTo>(gastoGuardado);
        }

        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }
    }
}