using GestionG.Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Service
{
    public interface IUsuarioService
    {
        Task<UsuarioDTo?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<UsuarioDTo>> ObtenerUsuariosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}
