using AutoMapper;
using GestionG.Domain.Entities;
using GestionG.Application.DTOs.Usuario;
using GestionG.Application.DTOs.Categoria;
using GestionG.Application.DTOs.Gasto;
using GestionG.Application.DTOs.Detalle;
using GestionG.Application.DTOs.Rol;

namespace GestionG.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            // USUARIOS
         
            CreateMap<Usuario, UsuarioDTo>()
               .ForMember(dest => dest.Rol, opt => opt.Ignore());

            CreateMap<UsuariosRegistroDto, Usuario>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

         
            // CATEGORIAS
         
            CreateMap<Categoria, CategoriaDTo>().ReverseMap();
            CreateMap<CategoriaCrearDTo, Categoria>();
            CreateMap<CategoriaActualizarDTo, Categoria>();

            
            // DETALLES 
           
            CreateMap<Detalle, DetalleDTo>()
            
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdDetalle))
                .ReverseMap();

            CreateMap<DetalleCrearDTo, Detalle>()
                .ForMember(dest => dest.IdDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdGasto, opt => opt.Ignore());

            CreateMap<DetalleCrearEnGastoDTo, Detalle>()
                .ForMember(dest => dest.IdDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdGasto, opt => opt.Ignore());
             
        
            // GASTOS
            CreateMap<Gasto, GastoDTo>()
           
                .ForMember(dest => dest.IdGasto, opt => opt.MapFrom(src => src.IdGasto))
                .ForMember(dest => dest.TotalGeneral, opt => opt.MapFrom(src => src.TotalGeneral))
                .ReverseMap();

            CreateMap<GastoCrearDTo, Gasto>()
                .ForMember(dest => dest.TotalGeneral, opt => opt.MapFrom(src => src.MontoTotal))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IdGasto, opt => opt.Ignore())
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));
        }
    }
}