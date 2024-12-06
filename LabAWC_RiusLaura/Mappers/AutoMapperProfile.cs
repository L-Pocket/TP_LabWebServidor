using AutoMapper;
using Entidades;
using LabAWS_RiusLaura.DTO;
using Restaurante_API.DTO;

namespace Restaurante_API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<DTO, Entidad>()
            this.CreateMap<PedidoCreateDto, Pedido>().ReverseMap();
            this.CreateMap<PedidoResponseDto, Pedido>().ReverseMap();

            //this.CreateMap<MesaDto, Mesa>().ReverseMap(); 
            CreateMap<Mesa, MesaDto>()
             .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.EstadoMesa.Descripcion))
             .ReverseMap();

            // Mapeo desde ComandaCrearDto a Comanda
            CreateMap<ComandaCrearDto, Comanda>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignoramos el IdComanda por ser un campo de identidad

            this.CreateMap<ComandaDto, Comanda>().ReverseMap();
            this.CreateMap<Empleado, EmpleadoCreateDto>().ReverseMap();

            CreateMap<Pedido, ClienteResponseDto>()
               .ForMember(dest => dest.tiempoEstimado, opt => opt.MapFrom(src => src.TiempoEstimado))
               .ForMember(dest => dest.tiempoDemorado, opt => opt.MapFrom(src => (int)Math.Round((DateTime.Now - src.FechaCreacion).TotalMinutes - src.TiempoEstimado, 0)));

            // Mapeo de Producto a ProductoVendidoDto
            CreateMap<Producto, ProductoVendidoDto>()
                .ForMember(dest => dest.NombreDesc, opt => opt.MapFrom(src => src.NombreDesc))
                .ForMember(dest => dest.CantidadVendida, opt => opt.Ignore()); // Ignoramos CantidadVendida ya que no proviene de la entidad Producto
                
            // Mapeo de Producto a ProductoPendienteDto
            CreateMap<Producto, ProductoPendienteDto>()
                .ForMember(dest => dest.NombreDesc, opt => opt.MapFrom(src => src.NombreDesc))                
                .ForMember(dest => dest.CantidadPendiente, opt => opt.Ignore()); // Ignoramos CantidadPendiente ya que no proviene de la entidad Producto
            
            //mapeo operaciones por sector (informe) -socioservicio
            CreateMap<OperacionesPorSectorDto, OperacionesPorSectorDto>();

            CreateMap<Empleado, LoginRequestDto>();

        }

        
        
    }
}
