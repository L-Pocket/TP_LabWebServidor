using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class LogueoEmpleadoSeed : IEntityTypeConfiguration<LogueoEmpleado>
    {
        public void Configure(EntityTypeBuilder<LogueoEmpleado> builder)
        {
            builder.HasData(
            new LogueoEmpleado
            {
                IdLogueo = 1,
                EmpleadoLogId = 1,
                FechaLogueo = new DateTime(2024, 8, 12, 19, 6, 0, DateTimeKind.Local),
                FechaDeslogueo = new DateTime(2024, 8, 12, 23, 59, 0, DateTimeKind.Local)
            },
            new LogueoEmpleado
            {
                IdLogueo = 2,
                EmpleadoLogId = 2,
                FechaLogueo = new DateTime(2024, 8, 12, 19, 1, 0, DateTimeKind.Local),
                FechaDeslogueo = new DateTime(2024, 8, 12, 23, 59, 0, DateTimeKind.Local)
            },
            new LogueoEmpleado
            {
                IdLogueo = 3,
                EmpleadoLogId = 3,
                FechaLogueo = new DateTime(2024, 8, 12, 17, 0, 0, DateTimeKind.Local),
                FechaDeslogueo = new DateTime(2024, 8, 12, 23, 49, 0, DateTimeKind.Local)
            },
            new LogueoEmpleado
            {
                IdLogueo = 4,
                EmpleadoLogId = 4,
                FechaLogueo = new DateTime(2024, 8, 12, 18, 16, 0, DateTimeKind.Local),
                FechaDeslogueo = new DateTime(2024, 8, 12, 23, 15, 0, DateTimeKind.Local)
            },
            new LogueoEmpleado
            {
                IdLogueo = 5,
                EmpleadoLogId = 5,
                FechaLogueo = new DateTime(2024, 8, 13, 19, 0, 0, DateTimeKind.Local),
                FechaDeslogueo = new DateTime(2024, 8, 13, 23, 33, 0, DateTimeKind.Local)
            },
            new LogueoEmpleado
            {
                IdLogueo = 6,
                EmpleadoLogId = 6,
                FechaLogueo = new DateTime(2024, 8, 13, 19, 30, 0, DateTimeKind.Local),
                FechaDeslogueo = new DateTime(2024, 8, 13, 23, 55, 0, DateTimeKind.Local)
            }
            );
        }
    }
}
