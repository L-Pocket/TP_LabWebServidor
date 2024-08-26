using Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.DAL.DataSeed
{
    public class MesaSeed : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            builder.HasData(
            new Mesa
            {
                IdMesa = 1,
                CodigoMesa = "M1001",
                EstadoDeMesaId = 1
            },
            new Mesa
            {
                IdMesa = 2,
                CodigoMesa = "M1002",
                EstadoDeMesaId = 1
            },
            new Mesa
            {
                IdMesa = 3,
                CodigoMesa = "M1003",
                EstadoDeMesaId = 4
            },
            new Mesa
            {
                IdMesa = 4,
                CodigoMesa = "M1004",
                EstadoDeMesaId = 1
            }
            );
        }
    }
}
