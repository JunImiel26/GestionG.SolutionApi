using GestionG.Application.DTOs.Detalle;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.DTOs.Gasto
{
    public class GastoDTo
    {
        public int IdGasto { get; set; } // Usa el mismo nombre que la entidad
        public DateTime Fecha { get; set; }
        public decimal TotalGeneral { get; set; } // Antes era TotalGeneral
        public List<DetalleDTo> Detalles { get; set; } = new();

    }
}
