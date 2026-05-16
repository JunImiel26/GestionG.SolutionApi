using GestionG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Repository
{
    public interface IRefreshTokenRepository
    {
        Task GuardarAsync(RefreshToken token);
        Task<RefreshToken?> ObtenerAsync(string token);
        Task ActualizarAsync(RefreshToken token);

    }
}
