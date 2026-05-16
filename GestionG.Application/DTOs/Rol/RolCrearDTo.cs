using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Rol
{
    internal class RolCrearDTo
    {
        [Required(ErrorMessage = "El nombre del rol es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre del rol no puede exceder los 50 caracteres.")]
        public string NombreRol { get; set; } = null!;
    }
}
