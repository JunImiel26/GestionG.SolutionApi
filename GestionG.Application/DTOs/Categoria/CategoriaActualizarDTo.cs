using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Categoria
{
    public class CategoriaActualizarDTo
    {
        [Required(ErrorMessage = "El ID de la categoría es requerido.")]
        public int IdCat { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

    }
}
