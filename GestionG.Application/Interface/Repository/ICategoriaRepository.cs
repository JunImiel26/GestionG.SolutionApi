using GestionG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Repository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObtenerTodasAsync();
        Task<Categoria?> ObtenerPorIdAsync(int id);
        Task<Categoria> InsertarAsync(Categoria categoria);
        Task ActualizarAsync(Categoria categoria);
        Task<bool> EliminarAsync(int id);

    }
}
