using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Service
{
    public interface IRolService
    {
        // Cambiado a IdentityRole<int>
        Task<IEnumerable<IdentityRole<int>>> ObtenerTodosAsync();

        // Cambiado el parámetro id a int y el retorno a IdentityRole<int>
        Task<IdentityRole<int>?> ObtenerPorIdAsync(int id);

        // Cambiado el retorno a IdentityRole<int>
        Task<IdentityRole<int>?> ObtenerPorNombreAsync(string nombre);

        Task<bool> CrearAsync(string nombreRol);

        // Cambiado el parámetro id a int
        Task<bool> ActualizarAsync(int id, string nuevoNombre);

        // Cambiado el parámetro id a int
        Task<bool> EliminarAsync(int id);
    }
}
