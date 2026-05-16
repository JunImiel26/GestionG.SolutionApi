using GestionG.Application.Interface.Repository;
using GestionG.Domain.Entities;
using GestionG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ObtenerAsync(string token)
        {
            return await _context.RefreshTokens
               .Include(x => x.Usuario)
               .FirstOrDefaultAsync(x => x.Token == token);
        }

    }
}
