using GestionG.Application.DTOs.Detalle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionG.Application.DTOs.Gasto
{
    public class GastoCrearDTo
    {
        public string Descripcion { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public int IdUsuario { get; set; }

        public List<DetalleCrearDTo> Detalles { get; set; } = new List<DetalleCrearDTo>();
    }

}

