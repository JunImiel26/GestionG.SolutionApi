using GestionG.Domain.Entities;

public class Detalle
{
    public int IdDetalle { get; set; }
    public decimal Monto { get; set; }
    public string Descripcion { get; set; } = null!;

    
    public int IdGasto { get; set; }
    public virtual Gasto Gasto { get; set; } = null!;

 
    public int IdCat { get; set; }
    public virtual Categoria Categoria { get; set; } = null!;
}