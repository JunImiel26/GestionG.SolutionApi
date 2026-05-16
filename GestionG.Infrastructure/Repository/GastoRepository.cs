using GestionG.Application.Interface.Repository;
using GestionG.Domain.Entities;
using GestionG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionG.Infrastructure.Repository
{
    public class GastoRepository : IGastoRepository
    {
        private readonly ApplicationDbContext _context;

        public GastoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gasto>> ObtenerTodosAsync()
        {
           
            return await _context.Gastos
                .Include(g => g.Detalles)
                .ThenInclude(d => d.Categoria) 
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Gasto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Gastos
                .Include(g => g.Detalles)
                .ThenInclude(d => d.Categoria)
                .FirstOrDefaultAsync(g => g.IdGasto == id);
        }

        public async Task<IEnumerable<Gasto>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _context.Gastos
        .Where(g => g.IdUsuario == idUsuario) // Ahora ambos son int, el error desaparece
        .Include(g => g.Detalles)
        .AsNoTracking()
        .ToListAsync();
        }

        public async Task<Gasto> InsertarAsync(Gasto gasto)
        {
            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
            return gasto;
        }

        public async Task ActualizarAsync(Gasto gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var filas = await _context.Gastos.Where(g => g.IdGasto == id).ExecuteDeleteAsync();
            return filas > 0;
        }
    }
}