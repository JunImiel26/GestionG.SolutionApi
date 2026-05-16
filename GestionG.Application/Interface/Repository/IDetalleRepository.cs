using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Repository
{
    public interface IDetalleRepository
    {
        Task<IEnumerable<Detalle>> ObtenerPorGastoAsync(int idGasto);
        Task<Detalle?> ObtenerPorIdAsync(int id);
        Task<Detalle> InsertarAsync(Detalle detalle);
        Task ActualizarAsync(Detalle detalle);
        Task<bool> EliminarAsync(int id);
    }
}
