using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class ProductoSeed : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasData(
            new Producto
            {
                Id = 1,
                SectorId = 1,
                NombreDesc = "Vino tinto Malbec",
                Stock = 50,
                Precio = 14000.00m
            },
            new Producto
            {
                Id = 2,
                SectorId = 1,
                NombreDesc = "Vino tinto Cabernet",
                Stock = 40,
                Precio = 14000.00m
            },
            new Producto
            {
                Id = 3,
                SectorId = 2,
                NombreDesc = "Cerveza artesanal IPA Roja",
                Stock = 200,
                Precio = 3700.00m
            },
            new Producto
            {
                Id = 4,
                SectorId = 2,
                NombreDesc = "Cerveza artesanal Negra",
                Stock = 150,
                Precio = 3700.00m
            },
            new Producto
            {
                Id = 5,
                SectorId = 3,
                NombreDesc = "Empanadas de Carne",
                Stock = 200,
                Precio = 1500.00m
            },
            new Producto
            {
                Id = 6,
                SectorId = 3,
                NombreDesc = "Empanadas de Verdura",
                Stock = 100,
                Precio = 1500.00m
            },
            new Producto
            {
                Id = 7,
                SectorId = 3,
                NombreDesc = "Empanadas de Pollo",
                Stock = 150,
                Precio = 1500.00m
            },
            new Producto
            {
                Id = 8,
                SectorId = 4,
                NombreDesc = "Postre Tiramisú",
                Stock = 40,
                Precio = 5000.00m
            },
            new Producto
            {
                Id = 9,
                SectorId = 4,
                NombreDesc = "Café",
                Stock = 400,
                Precio = 2500.00m
            }
            );
        }
    }
}
