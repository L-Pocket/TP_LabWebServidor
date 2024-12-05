using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class EmpleadoSeed : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
           builder.HasData(
           new Empleado
           {
               Id = 1,
               Nombre = "Juan Pérez",
               Usuario = "bartender1",
               Password = "bartender1",
               SectorId = 1,
               RolId = 1,
               EmpleadoActivo = true
           },
           new Empleado
           {
               Id = 2,
               Nombre = "María Gómez",
               Usuario = "cervecero1",
               Password = "cervecero1",
               SectorId = 2,
               RolId = 2,
               EmpleadoActivo = true

           },
           new Empleado
           {
               Id = 3,
               Nombre = "Carlos López",
               Usuario = "cocinero1",
               Password = "cocinero1",
               SectorId = 3,
               RolId = 3,
               EmpleadoActivo = true

           },
           new Empleado
           {
               Id = 4,
               Nombre = "Ana Martínez",
               Usuario = "mozo1",
               Password = "mozo1",
               SectorId = 4,
               RolId = 4,
               EmpleadoActivo = true

           },
           new Empleado
           {
               Id = 5,
               Nombre = "Jorge García",
               Usuario = "socio1",
               Password = "socio1",
               SectorId = 5,
               RolId = 5,
               EmpleadoActivo = true

           },
           new Empleado
           {
               Id = 6,
               Nombre = "Laura Torres",
               Usuario = "bartender2",
               Password = "bartender2",
               SectorId = 1,
               RolId = 1,
               EmpleadoActivo = false

           },
           new Empleado
           {
               Id = 7,
               Nombre = "Esteban Rodriguez",
               Usuario = "socio2",
               Password = "socio2",
               SectorId = 5,
               RolId = 5,
               EmpleadoActivo = true

           },
           new Empleado
           {
               Id = 8,
               Nombre = "Pedro Ramirez",
               Usuario = "cocinero2",
               Password = "cocinero2",
               SectorId = 3,
               RolId = 3,
               EmpleadoActivo = true

           },
           new Empleado
           {
               Id = 9,
               Nombre = "Gonzalo Fernandez",
               Usuario = "socio3",
               Password = "socio3",
               SectorId = 5,
               RolId = 5,
               EmpleadoActivo = false

           }
           );
        }
    }
}
