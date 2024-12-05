using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class RolSeed : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasData(
            new Rol
            {
                Id = 1,
                Descripcion = "Bartender"
            },
            new Rol
            {
                Id = 2,
                Descripcion = "Cervecero"
            },
            new Rol
            {
                Id = 3,
                Descripcion = "Cocinero"
            },
            new Rol
            {
                Id = 4,
                Descripcion = "Mozo"
            },
            new Rol
            {
                Id = 5,
                Descripcion = "Socio"
            }
            );
        }
    }
}
