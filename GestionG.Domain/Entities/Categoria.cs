using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestionG.Domain.Entities
{
    public class Categoria
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCat { get; set; }
        public string Nombre { get; set; } = null!; 

        public ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    }

}
