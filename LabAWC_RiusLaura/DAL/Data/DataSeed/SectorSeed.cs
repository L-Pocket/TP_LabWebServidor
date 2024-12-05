using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class SectorSeed : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.HasData(
            new Sector
            {
                Id = 1,
                Descripcion = "Barra Tragos Y Vino"
            },
            new Sector
            {
                Id = 2,
                Descripcion = "Cerveza Artesanal"
            },
            new Sector
            {
                Id = 3,
                Descripcion = "Cocina"
            },
            new Sector
            {
                Id = 4,
                Descripcion = "Candybar"
            },
            new Sector
            {
                Id = 5,
                Descripcion = "General"
            }
            );
        }
    }
}
