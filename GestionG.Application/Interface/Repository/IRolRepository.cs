using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Repository
{
    public interface IRolRepository
    {
        Task<IEnumerable<IdentityRole<int>>> ObtenerTodosAsync();
        Task<IdentityRole<int>?> ObtenerPorIdAsync(int IdRol);
        Task<IdentityRole<int>?> ObtenerPorNombreAsync(string NombreRol);
        Task<IdentityRole<int>> InsertarAsync(IdentityRole<int> entidad);
        Task ActualizarAsync(IdentityRole<int> entidad);
        Task<bool> EliminarAsync(int id); // Cambiado a int
    }
}
