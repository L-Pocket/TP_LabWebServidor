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
                IdProducto = 1,
                SectorProductoId = 1,
                NombreDescProducto = "Vino tinto Malbec",
                StockProducto = 50,
                PrecioProducto = 14000.00m
            },
            new Producto
            {
                IdProducto = 2,
                SectorProductoId = 1,
                NombreDescProducto = "Vino tinto Cabernet",
                StockProducto = 40,
                PrecioProducto = 14000.00m
            },
            new Producto
            {
                IdProducto = 3,
                SectorProductoId = 2,
                NombreDescProducto = "Cerveza artesanal IPA Roja",
                StockProducto = 200,
                PrecioProducto = 3700.00m
            },
            new Producto
            {
                IdProducto = 4,
                SectorProductoId = 2,
                NombreDescProducto = "Cerveza artesanal Negra",
                StockProducto = 150,
                PrecioProducto = 3700.00m
            },
            new Producto
            {
                IdProducto = 5,
                SectorProductoId = 3,
                NombreDescProducto = "Empanadas de Carne",
                StockProducto = 200,
                PrecioProducto = 1500.00m
            },
            new Producto
            {
                IdProducto = 6,
                SectorProductoId = 3,
                NombreDescProducto = "Empanadas de Verdura",
                StockProducto = 100,
                PrecioProducto = 1500.00m
            },
            new Producto
            {
                IdProducto = 7,
                SectorProductoId = 3,
                NombreDescProducto = "Empanadas de Pollo",
                StockProducto = 150,
                PrecioProducto = 1500.00m
            },
            new Producto
            {
                IdProducto = 8,
                SectorProductoId = 4,
                NombreDescProducto = "Postre Tiramisú",
                StockProducto = 40,
                PrecioProducto = 5000.00m
            },
            new Producto
            {
                IdProducto = 9,
                SectorProductoId = 4,
                NombreDescProducto = "Café",
                StockProducto = 400,
                PrecioProducto = 2500.00m
            }
            );
        }
    }
}
