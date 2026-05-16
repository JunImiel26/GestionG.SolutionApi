using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Detalle
{
    public class DetalleActualizarDTo
    {
        [Required(ErrorMessage = "El ID del detalle es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El monto es requerido.")]
        [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        [MaxLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La categoría es requerida.")]
        public int IdCat { get; set; }
    }
}
