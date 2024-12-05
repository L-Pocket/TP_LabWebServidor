using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabAWC_RiusLaura.DAL.Data.DataSeed
{
    public class ComandaSeed : IEntityTypeConfiguration<Comanda>
    {
        public void Configure(EntityTypeBuilder<Comanda> builder)
        {
             builder.HasData(
             new Comanda
             {
                 Id = 1,
                 MesaId = 1,
                 NombreCliente = "Cliente A",
             },
             new Comanda
             {
                 Id = 2,
                 MesaId = 2,
                 NombreCliente = "Cliente B"
             },
             new Comanda
             {
                 Id = 3,
                 MesaId = 3,
                 NombreCliente = "Cliente C"
             },
             new Comanda
             {
                 Id = 4,
                 MesaId = 4,
                 NombreCliente = "Cliente D"
             },
             new Comanda
             {

                 Id = 5,
                 MesaId = 1,
                 NombreCliente = "Cliente E"
             }
             );
        }
    }
}
