using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Detalle
{
    public class DetalleCrearDTo
    {
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdCat { get; set; }
        public int IdGasto { get; set; }

    }
}
