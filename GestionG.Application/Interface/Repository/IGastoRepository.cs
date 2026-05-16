using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Repository
{
    public interface IGastoRepository
    {
        Task<IEnumerable<Gasto>> ObtenerTodosAsync();
        Task<Gasto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Gasto>> ObtenerPorUsuarioAsync(int idUsuario); 
        Task ActualizarAsync(Gasto gasto);
        Task<bool> EliminarAsync(int id);
        Task<Gasto> InsertarAsync(Gasto gasto);
    }
}
