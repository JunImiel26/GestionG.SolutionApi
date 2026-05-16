using GestionG.Application.DTOs.Gasto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Service
{
    public interface IGastoService
    {
        Task<GastoDTo?> ObterPorId(int id);
        Task<IEnumerable<GastoDTo>> ObtenerTodosAsync();
        Task<IEnumerable<GastoDTo>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<GastoDTo> CrearAsync(GastoCrearDTo dto);
        Task EliminarAsync(int id);
    }
}
