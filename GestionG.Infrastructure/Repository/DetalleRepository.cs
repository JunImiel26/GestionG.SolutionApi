using GestionG.Application.Interface.Repository;
using GestionG.Domain.Entities;
using GestionG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionG.Infrastructure.Repository
{
    public class DetalleRepository : IDetalleRepository
    {
        private readonly ApplicationDbContext _context;

        public DetalleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Detalle>> ObtenerPorGastoAsync(int idGasto)
        {
            return await _context.Detalles
                .Where(d => d.IdGasto == idGasto)
                .Include(d => d.Categoria) 
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Detalle?> ObtenerPorIdAsync(int id)
        {
            return await _context.Detalles.FindAsync(id);
        }

        public async Task<Detalle> InsertarAsync(Detalle detalle)
        {
            await _context.Detalles.AddAsync(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task ActualizarAsync(Detalle detalle)
        {
            _context.Detalles.Update(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var filas = await _context.Detalles.Where(d => d.IdDetalle == id).ExecuteDeleteAsync();
            return filas > 0;
        }
    }
}