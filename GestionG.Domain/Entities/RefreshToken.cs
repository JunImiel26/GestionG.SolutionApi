using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expiracion { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
