using AutoMapper;
using Microsoft.AspNetCore.Identity;
namespace GestionG.Application.DTOs.Rol
{
    public class RolDTo
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
    }
}
