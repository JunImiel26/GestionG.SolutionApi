using GestionG.Domain.Entities;

public class Rol
{
   
    public int IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public ICollection<Usuario> Usuarios { get; set; } = null!;
}