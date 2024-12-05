using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class PedidoSeed : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasData(
           new Pedido
           {
               Id = 1,
               ComandaId = 1,
               ProductoId = 1,
               Cantidad = 1,
               EstadoPedidoId = 1,
               FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
               //
               TiempoEstimado = 10,               
               Observaciones = "Con hielo"
           },
           new Pedido
           {
               Id = 2,
               ComandaId = 1,
               ProductoId = 3,
               Cantidad = 2,
               EstadoPedidoId = 1,
               FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
               //
               TiempoEstimado = 20,               
               Observaciones = ""
           },
           new Pedido
           {
               Id = 3,
               ComandaId = 1,
               ProductoId = 3,
               Cantidad = 3,
               EstadoPedidoId = 1,
               FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
               //
               TiempoEstimado = 30,               
               Observaciones = ""
           },
           new Pedido
           {
               Id = 4,
               ComandaId = 2,
               ProductoId = 8,
               Cantidad = 2,
               EstadoPedidoId = 1,
               FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
               //
               TiempoEstimado = 15,               
               Observaciones = ""
           },
           new Pedido
           {
               Id = 5,
               ComandaId = 3,
               ProductoId = 9,
               Cantidad = 2,
               EstadoPedidoId = 1,
               FechaCreacion = new DateTime(2024, 8, 12, 19, 30, 0, DateTimeKind.Local),
               //
               TiempoEstimado = 40,               
               Observaciones = ""
           },
           new Pedido
           {
               Id = 6,
               ComandaId = 5,
               ProductoId = 2,
               Cantidad = 4,
               EstadoPedidoId = 4,
               FechaCreacion = new DateTime(2024, 8, 24, 19, 30, 0, DateTimeKind.Local),
               FechaFinalizacion = new DateTime(2024, 8, 24, 19, 47, 0, DateTimeKind.Local),
               TiempoEstimado = 15,               
               Observaciones = "bien frío"
           }

           );
        }
    }
}
