using GestionG.Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ObtenerUsuariosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}
