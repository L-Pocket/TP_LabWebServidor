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
                IdSector = 1,
                DescripcionSector = "Barra Tragos Y Vino"
            },
            new Sector
            {
                IdSector = 2,
                DescripcionSector = "Cerveza Artesanal"
            },
            new Sector
            {
                IdSector = 3,
                DescripcionSector = "Cocina"
            },
            new Sector
            {
                IdSector = 4,
                DescripcionSector = "Candybar"
            },
            new Sector
            {
                IdSector = 5,
                DescripcionSector = "General"
            }
            );
        }
    }
}
