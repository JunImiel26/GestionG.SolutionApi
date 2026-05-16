using GestionG.Application.DTOs.Rol;
using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Mappings
{
    public class RolService : IRolService
    {
        // Asegúrate de que el RoleManager tenga el <int>
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RolService(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<IEnumerable<IdentityRole<int>>> ObtenerTodosAsync()
    => Task.FromResult(_roleManager.Roles.AsEnumerable());

        public async Task<IdentityRole<int>?> ObtenerPorIdAsync(int id) // Cambiado a int
            => await _roleManager.FindByIdAsync(id.ToString());

        public async Task<IdentityRole<int>?> ObtenerPorNombreAsync(string nombre)
            => await _roleManager.FindByNameAsync(nombre);

        public async Task<bool> CrearAsync(string nombreRol)
        {
            // EL CAMBIO CLAVE: Debes instanciar IdentityRole<int>
            var nuevoRol = new IdentityRole<int>
            {
                Name = nombreRol
            };

            var result = await _roleManager.CreateAsync(nuevoRol);
            return result.Succeeded;
        }


        public async Task<bool> EliminarAsync(string id)
        {
            var rol = await _roleManager.FindByIdAsync(id);
            if (rol == null) return false;

            var result = await _roleManager.DeleteAsync(rol);
            return result.Succeeded;
        }

        public async Task<bool> ActualizarAsync(int id, string nuevoNombre)
        {
            // 1. Convertimos el int a string porque FindByIdAsync lo requiere así
            var rol = await _roleManager.FindByIdAsync(id.ToString());

            if (rol == null)
            {
                // Podrías lanzar una excepción personalizada aquí si prefieres
                return false;
            }

            // 2. Actualizamos el nombre y el NormalizerName 
            // (UpdateAsync se encarga de normalizarlo, pero es buena práctica asignarlo)
            rol.Name = nuevoNombre;

            // 3. Ejecutamos la actualización
            var result = await _roleManager.UpdateAsync(rol);

            // 4. Manejo de errores (opcional pero recomendado para depurar)
            if (!result.Succeeded)
            {
                var errores = string.Join(", ", result.Errors.Select(e => e.Description));
                // Aquí podrías usar un Logger para registrar los errores de Identity
                return false;
            }

            return true;
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}


