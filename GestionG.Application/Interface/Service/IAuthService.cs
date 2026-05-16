using GestionG.Application.DTOs.Usuario;
using GestionG.Application.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Service
{
    public interface IAuthService
    {

        Task<RespuestaLoginDto> LoginAsync(UsuarioLoginDTo dto);
        Task<UsuarioDTo> RegistrarUsuarioAsync(UsuariosRegistroDto dto);
        Task<RespuestaLoginDto> RefreshTokenAsync(string refreshToken);

    }
}
