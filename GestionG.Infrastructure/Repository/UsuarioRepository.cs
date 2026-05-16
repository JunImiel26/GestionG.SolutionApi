using GestionG.Application.Interface.Repository;
using GestionG.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static GestionG.Infrastructure.Repository.UsuarioRepository;

namespace GestionG.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
       
       
            private readonly ApplicationDbContext _context;

            public UsuarioRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> ContarAsync()
            {
                return await _context.Users.CountAsync();
            }

            public async Task<Usuario?> ObtenerPorIdAsync(string id)
            {
                return await _context.Users.FindAsync(id);
            }

            public async Task<Usuario?> ObtenerPorIdAsync(int id)
            {
                return await _context.Users.FindAsync(id);
            }

            public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync(int pagina, int tamano)
            {
                return await _context.Users
                   .AsNoTracking()
                   .OrderBy(u => u.UserName)
                   .Skip((pagina - 1) * tamano)
                   .Take(tamano)
                   .ToListAsync();
            }
    }

}




