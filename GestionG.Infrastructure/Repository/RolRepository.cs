using GestionG.Application.Interface.Repository;
using GestionG.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestionG.Infrastructure.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RolRepository(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        // Cambiado a IdentityRole<int>
        public async Task<IEnumerable<IdentityRole<int>>> ObtenerTodosAsync()
        {
            return await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        // Cambiado a IdentityRole<int>
        public async Task<IdentityRole<int>?> ObtenerPorIdAsync(int IdRol)
        {
            // El Manager espera un string para el ID, así que convertimos el int
            return await _roleManager.FindByIdAsync(IdRol.ToString());
        }

        // Cambiado a IdentityRole<int>
        public async Task<IdentityRole<int>?> ObtenerPorNombreAsync(string NombreRol)
        {
            return await _roleManager.FindByNameAsync(NombreRol);
        }

        // El parámetro debe ser IdentityRole<int>
        public async Task<IdentityRole<int>> InsertarAsync(IdentityRole<int> entidad)
        {
            var result = await _roleManager.CreateAsync(entidad);
            if (result.Succeeded) return entidad;

            throw new Exception($"Error al crear rol: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // El parámetro debe ser IdentityRole<int>
        public async Task ActualizarAsync(IdentityRole<int> entidad)
        {
            await _roleManager.UpdateAsync(entidad);
        }

        // Cambiado el parámetro a int para ser consistente con el resto del repo
        public async Task<bool> EliminarAsync(int id)
        {
            var rol = await _roleManager.FindByIdAsync(id.ToString());
            if (rol == null) return false;

            var result = await _roleManager.DeleteAsync(rol);
            return result.Succeeded;
        }
    }
}
   
