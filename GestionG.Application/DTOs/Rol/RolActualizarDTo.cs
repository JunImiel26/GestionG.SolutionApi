using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Rol
{
    internal class RolActualizarDTo
    {
        [Required(ErrorMessage = "El ID del rol es requerido.")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre del rol no puede exceder los 50 caracteres.")]
        public string NombreRol { get; set; } = null!;
    }
}
