using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Usuario
{
    internal class UsuarioActualizarDTo
    {

        [Required(ErrorMessage = "El ID del usuario es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [MaxLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El rol es requerido.")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public bool Estado { get; set; }
    }
}
