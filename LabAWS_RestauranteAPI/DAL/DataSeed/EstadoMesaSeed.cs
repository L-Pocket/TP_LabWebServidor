using Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.DAL.DataSeed
{
    public class EstadoMesaSeed : IEntityTypeConfiguration<EstadoMesa>
    {
        public void Configure(EntityTypeBuilder<EstadoMesa> builder)
        {
            builder.HasData(
            new EstadoMesa
            {
                IdEstadoMesa = 1,
                DescripcionMesa = "Cliente Esperando Pedido"
            },
            new EstadoMesa
            {
                IdEstadoMesa = 2,
                DescripcionMesa = "Cliente Comiendo"
            },
            new EstadoMesa
            {
                IdEstadoMesa = 3,
                DescripcionMesa = "Cliente Pagando"
            },
            new EstadoMesa
            {
                IdEstadoMesa = 4,
                DescripcionMesa = "Cerrada"
            }
            );
        }
    }
}
