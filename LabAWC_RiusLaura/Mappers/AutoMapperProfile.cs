﻿using AutoMapper;
using Entidades;
using LabAWS_RiusLaura.DTO;

namespace Restaurante_API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<DTO, Entidad>()
            this.CreateMap<PedidoCreateDto, Pedido>().ReverseMap();
            this.CreateMap<PedidoResponseDto, Pedido>().ReverseMap();
            this.CreateMap<MesaDto, Mesa>().ReverseMap();
            this.CreateMap<ComandaDto, Comanda>().ReverseMap();

            CreateMap<Pedido, ClienteResponseDto>()
               .ForMember(dest => dest.tiempoEstimado, opt => opt.MapFrom(src => src.TiempoEstimado))
               .ForMember(dest => dest.tiempoDemorado, opt => opt.MapFrom(src => (int)Math.Round((DateTime.Now - src.FechaCreacion).TotalMinutes - src.TiempoEstimado, 0)));

            // Mapeo de Producto a ProductoVendidoDto
            CreateMap<Producto, ProductoVendidoDto>()
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.NombreDescProducto, opt => opt.MapFrom(src => src.NombreDescProducto))
                .ForMember(dest => dest.CantidadVendida, opt => opt.Ignore()); // Ignoramos CantidadVendida ya que no proviene de la entidad Producto

        }

        //public PedidoCreateDto ConvertirADTO(Pedido pedido)
        //{
        //    PedidoCreateDto dto = new PedidoCreateDto();
        //    dto.ProductoDelPedidoId = pedido.ProductoDelPedidoId;
        //    dto.ComandaDelPedidoId = pedido.ComandaDelPedidoId;
        //    dto.Cantidad = pedido.Cantidad;
        //    dto.CodigoCliente = pedido.CodigoCliente;
        //    dto.ObservacionesDelPedido = pedido.ObservacionesDelPedido;               

        //    return dto; 

        //}
        //public Pedido ConvertirAEntidad(PedidoCreateDto pedidoDto)
        //{
        //    Pedido ent = new Pedido();
        //    ent.ProductoDelPedidoId = pedidoDto.ProductoDelPedidoId;
        //    ent.ComandaDelPedidoId = pedidoDto.ComandaDelPedidoId;
        //    ent.Cantidad = pedidoDto.Cantidad;
        //    ent.CodigoCliente = pedidoDto.CodigoCliente;
        //    ent.ObservacionesDelPedido = pedidoDto.ObservacionesDelPedido;

        //    return ent;

        //}
    }
}
