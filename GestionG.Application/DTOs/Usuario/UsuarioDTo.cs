using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.DTOs.Usuario
{
    public class UsuarioDTo
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Estado { get; set; }

        public string Rol { get; set; } = null!;
    }
}
