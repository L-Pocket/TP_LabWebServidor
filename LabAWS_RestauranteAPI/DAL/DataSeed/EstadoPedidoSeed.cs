using Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.DAL.DataSeed
{
    public class EstadoPedidoSeed : IEntityTypeConfiguration<EstadoPedido>
    {
        public void Configure(EntityTypeBuilder<EstadoPedido> builder)
        {
            builder.HasData(
            new EstadoPedido
            {
                IdEstadoPedido = 1,
                DescripcionPedido = "Pendiente"
            },
            new EstadoPedido
            {
                IdEstadoPedido = 2,
                DescripcionPedido = "En Preparacion"
            },
            new EstadoPedido
            {
                IdEstadoPedido = 3,
                DescripcionPedido = "Listo Para Servir"
            },
            new EstadoPedido
            {
                IdEstadoPedido = 4,
                DescripcionPedido = "Servido"
            },
            new EstadoPedido
            {
                IdEstadoPedido = 5,
                DescripcionPedido = "Cancelado"
            }
            );
        }
    }
}
