
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Usuario
{
    public class UsuarioLoginDTo
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del email no es valido.")]
        [MaxLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El rol es requerido.")]
        public int IdRol { get; set; }
    }
}
