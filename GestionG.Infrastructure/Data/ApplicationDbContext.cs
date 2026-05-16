using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionG.Domain.Entities;

namespace GestionG.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Tablas de Negocio
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Detalle> Detalles { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ==========================================
            // CONFIGURACIÓN: CATEGORÍA
            // ==========================================
            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.IdCat);
                entity.Property(c => c.IdCat).UseIdentityByDefaultColumn();
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            });

            // ==========================================
            // CONFIGURACIÓN: GASTO
            // ==========================================
            builder.Entity<Gasto>(entity =>
            {
                entity.HasKey(g => g.IdGasto);
                entity.Property(g => g.IdGasto).UseIdentityByDefaultColumn();

                entity.Property(g => g.TotalGeneral).HasPrecision(18, 2);

                // Relación Gasto -> Usuario (1:N)
                // El usuario es el dueño del gasto completo.
                entity.HasOne(g => g.Usuario)
                    .WithMany(u => u.Gastos)
                    .HasForeignKey(g => g.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // CONFIGURACIÓN: DETALLE
            // ==========================================
            builder.Entity<Detalle>(entity =>
            {
                entity.HasKey(d => d.IdDetalle);
                entity.Property(d => d.IdDetalle).UseIdentityByDefaultColumn();

                entity.Property(d => d.Monto).HasPrecision(18, 2);
                entity.Property(d => d.Descripcion).IsRequired().HasMaxLength(250);

                // Relación Detalle -> Gasto (N:1)
                // Si se elimina el Gasto, se eliminan sus detalles (Cascade)
                entity.HasOne(d => d.Gasto)
                    .WithMany(g => g.Detalles)
                    .HasForeignKey(d => d.IdGasto)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación Detalle -> Categoria (N:1)
                entity.HasOne(d => d.Categoria)
                    .WithMany(c => c.Detalles)
                    .HasForeignKey(d => d.IdCat)
                    .OnDelete(DeleteBehavior.Restrict);

                // 💡 NOTA: Se eliminó la relación directa Detalle -> Usuario
                // para evitar redundancia y errores de Foreign Key en PostgreSQL.
            });

            // ==========================================
            // CONFIGURACIÓN: REFRESH TOKEN
            // ==========================================
            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.HasOne(rt => rt.Usuario)
                      .WithMany()
                      .HasForeignKey(rt => rt.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}