using Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.DAL.DataSeed
{
    public class PedidoSeed : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasData(
            new Pedido
            {
                IdPedido = 1,
                ComandaDelPedidoId = 1,
                ProductoDelPedidoId = 1,
                Cantidad = 1,
                EstadoDelPedidoId = 1,
                FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
                //
                TiempoEstimado = 10,
                CodigoCliente = "MBC12",
                ObservacionesDelPedido = "Con hielo"
            },
            new Pedido
            {
                IdPedido = 2,
                ComandaDelPedidoId = 1,
                ProductoDelPedidoId = 3,
                Cantidad = 2,
                EstadoDelPedidoId = 1,
                FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
                //
                TiempoEstimado = 20,
                CodigoCliente = "MBC12",
                ObservacionesDelPedido = ""
            },
            new Pedido
            {
                IdPedido = 3,
                ComandaDelPedidoId = 1,
                ProductoDelPedidoId = 3,
                Cantidad = 3,
                EstadoDelPedidoId = 1,
                FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
                //
                TiempoEstimado = 30,
                CodigoCliente = "MBC12",
                ObservacionesDelPedido = ""
            },
            new Pedido
            {
                IdPedido = 4,
                ComandaDelPedidoId = 2,
                ProductoDelPedidoId = 8,
                Cantidad = 2,
                EstadoDelPedidoId = 1,
                FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
                //
                TiempoEstimado = 15,
                CodigoCliente = "AD32S",
                ObservacionesDelPedido = ""
            },
            new Pedido
            {
                IdPedido = 5,
                ComandaDelPedidoId = 3,
                ProductoDelPedidoId = 9,
                Cantidad = 2,
                EstadoDelPedidoId = 1,
                FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
                //
                TiempoEstimado = 40,
                CodigoCliente = "KAE2K",
                ObservacionesDelPedido = ""
            },
            new Pedido
            {
                IdPedido = 6,
                ComandaDelPedidoId = 5,
                ProductoDelPedidoId = 2,
                Cantidad = 4,
                EstadoDelPedidoId = 4,
                FechaCreacion = new DateTime(2024, 8, 24, 19, 30, 0, DateTimeKind.Local),
                FechaFinalizacion = new DateTime(2024, 8, 24, 19, 47, 0, DateTimeKind.Local),
                TiempoEstimado = 15,
                CodigoCliente = "TYH3K",
                ObservacionesDelPedido = "bien frío"
            }

            );
        }
    }
}
