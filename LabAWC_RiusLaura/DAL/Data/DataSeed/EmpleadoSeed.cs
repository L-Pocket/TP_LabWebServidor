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
                IdEmpleado = 1,
                Nombre = "Juan Pérez",
                Usuario = "jperez",
                Password = "password1",
                SectorDelEmpleadoId = 1,
                RolDelEmpleadoId = 1,
                EmpleadoActivo = true
            },
            new Empleado
            {
                IdEmpleado = 2,
                Nombre = "María Gómez",
                Usuario = "mgomez",
                Password = "password2",
                SectorDelEmpleadoId = 2,
                RolDelEmpleadoId = 2,
                EmpleadoActivo = true

            },
            new Empleado
            {
                IdEmpleado = 3,
                Nombre = "Carlos López",
                Usuario = "clopez",
                Password = "password3",
                SectorDelEmpleadoId = 3,
                RolDelEmpleadoId = 3,
                EmpleadoActivo = true

            },
            new Empleado
            {
                IdEmpleado = 4,
                Nombre = "Ana Martínez",
                Usuario = "amartinez",
                Password = "password4",
                SectorDelEmpleadoId = 4,
                RolDelEmpleadoId = 4,
                EmpleadoActivo = true

            },
            new Empleado
            {
                IdEmpleado = 5,
                Nombre = "Jorge García",
                Usuario = "jgarcia",
                Password = "password5",
                SectorDelEmpleadoId = 5,
                RolDelEmpleadoId = 5,
                EmpleadoActivo = true

            },
            new Empleado
            {
                IdEmpleado = 6,
                Nombre = "Laura Torres",
                Usuario = "ltorres",
                Password = "password6",
                SectorDelEmpleadoId = 1,
                RolDelEmpleadoId = 1,
                EmpleadoActivo = false

            },
            new Empleado
            {
                IdEmpleado = 7,
                Nombre = "Esteban Rodriguez",
                Usuario = "erodriguez",
                Password = "password7",
                SectorDelEmpleadoId = 5,
                RolDelEmpleadoId = 5,
                EmpleadoActivo = true

            },
            new Empleado
            {
                IdEmpleado = 8,
                Nombre = "Pedro Ramirez",
                Usuario = "pramirez",
                Password = "password8",
                SectorDelEmpleadoId = 3,
                RolDelEmpleadoId = 3,
                EmpleadoActivo = true

            },
            new Empleado
            {
                IdEmpleado = 9,
                Nombre = "Gonzalo Fernandez",
                Usuario = "gfernandez",
                Password = "password9",
                SectorDelEmpleadoId = 5,                
                RolDelEmpleadoId = 5,
                EmpleadoActivo = false

            }
            );
        }
    }
}
