using GestionG.Domain.Entities;

public class Gasto
{
    public int IdGasto { get; set; }
    public decimal TotalGeneral { get; set; }
    public DateTime Fecha { get; set; }

    
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();
}


