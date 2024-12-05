using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class MesaSeed : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            builder.HasData(
            new Mesa
            {
                Id = 1,                
                EstadoMesaId = 1
            },
            new Mesa
            {
                Id = 2,                
                EstadoMesaId = 1
            },
            new Mesa
            {
                Id = 3,                
                EstadoMesaId = 4
            },
            new Mesa
            {
                Id = 4,                
                EstadoMesaId = 1
            }
            );
        }
    }
}
