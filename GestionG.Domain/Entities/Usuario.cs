
using Microsoft.AspNetCore.Identity;



public class Usuario:IdentityUser<int>
{
  
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; } = true;

        
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    
}
