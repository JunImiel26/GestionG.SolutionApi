using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GestionG.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}