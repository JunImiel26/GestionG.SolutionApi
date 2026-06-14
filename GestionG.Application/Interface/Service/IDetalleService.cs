using GestionG.Application.DTOs.Detalle;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Service
{
    public interface IDetalleService
    {
        Task<DetalleDTo?> ObterPorId(int id);
        Task<IEnumerable<DetalleDTo>> ObtenerPorGastoAsync(int idGasto);
        Task<DetalleDTo> CrearAsync(DetalleCrearDTo dto, int? currentUserId = null);
        Task EliminarAsync(int id);
    }
}
