using AutoMapper;
using GestionG.Application.DTOs.Usuario;
using GestionG.Application.Interface.Repository;
using GestionG.Application.Interface.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionG.Application.Mappings
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> ContarAsync()
        {
            return await _repository.ContarAsync();
        }

        public async Task<UsuarioDTo?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID es requerido.");
            }

            var registro = await _repository.ObtenerPorIdAsync(id);

            if (registro == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            return _mapper.Map<UsuarioDTo>(registro);
        }

        public async Task<IEnumerable<UsuarioDTo>> ObtenerUsuariosAsync(int pagina, int tamano)
        {
            var registros = await _repository.ObtenerUsuariosAsync(pagina, tamano);
            return _mapper.Map<IEnumerable<UsuarioDTo>>(registros);
        }

       
    }
}

