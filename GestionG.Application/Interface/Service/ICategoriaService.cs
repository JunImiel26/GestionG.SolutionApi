using GestionG.Application.DTOs.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Interface.Service
{
    public interface ICategoriaService
    {
        Task<CategoriaDTo?> ObterPorId(int id);
        Task<IEnumerable<CategoriaDTo>> ObtenerTodasAsync();
        Task<IEnumerable<CategoriaDTo>> BuscarCategoriaAsync(string nombre);


        Task<CategoriaDTo> CrearAsync(CategoriaCrearDTo dto);
        Task<CategoriaDTo> ActualizarAsync(int id, CategoriaActualizarDTo dto);
        Task EliminarAsync(int id);

    }
}
