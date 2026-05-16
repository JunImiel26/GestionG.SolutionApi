using GestionG.Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Response
{
    public class RespuestaLoginDto
    {
        public UsuarioDTo Usuario { get; set; } = null!;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiraEn { get; set; }
    }
}
