using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class EstadoMesaSeed : IEntityTypeConfiguration<EstadoMesa>
    {
        public void Configure(EntityTypeBuilder<EstadoMesa> builder)
        {
            builder.HasData(
            new EstadoMesa
            {
                Id = 1,
                Descripcion = "Cliente Esperando Pedido"
            },
            new EstadoMesa
            {
                Id = 2,
                Descripcion = "Cliente Comiendo"
            },
            new EstadoMesa
            {
                Id = 3,
                Descripcion = "Cliente Pagando"
            },
            new EstadoMesa
            {
                Id = 4,
                Descripcion = "Cerrada"
            }
            );
        }
    }
}
