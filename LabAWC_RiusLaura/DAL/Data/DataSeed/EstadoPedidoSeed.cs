using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class EstadoPedidoSeed : IEntityTypeConfiguration<EstadoPedido>
    {
        public void Configure(EntityTypeBuilder<EstadoPedido> builder)
        {
            builder.HasData(
            new EstadoPedido
            {
                Id = 1,
                Descripcion = "Pendiente"
            },
            new EstadoPedido
            {
                Id = 2,
                Descripcion = "En Preparacion"
            },
            new EstadoPedido
            {
                Id = 3,
                Descripcion = "Listo Para Servir"
            },
            new EstadoPedido
            {
                Id = 4,
                Descripcion = "Servido"
            },
            new EstadoPedido
            {
                Id = 5,
                Descripcion = "Cancelado"
            }
            );
        }
    }
}
