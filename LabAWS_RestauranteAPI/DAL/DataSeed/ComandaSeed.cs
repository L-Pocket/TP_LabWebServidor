using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LabAWS_RestauranteAPI.DAL.DataSeed
{
    public class ComandaSeed : IEntityTypeConfiguration<Comanda>
    {
        public void Configure(EntityTypeBuilder<Comanda> builder)
        {
            builder.HasData(
            new Comanda
            {

                IdComanda = 1,
                MesaDeComandaId = 1,
                NombreCliente = "Cliente A",
            },
            new Comanda
            {
                IdComanda = 2,
                MesaDeComandaId = 2,
                NombreCliente = "Cliente B"
            },
            new Comanda
            {
                IdComanda = 3,
                MesaDeComandaId = 3,
                NombreCliente = "Cliente C"
            },
            new Comanda
            {
                IdComanda = 4,
                MesaDeComandaId = 4,
                NombreCliente = "Cliente D"
            },
            new Comanda
            {

                IdComanda = 5,
                MesaDeComandaId = 1,
                NombreCliente = "Cliente E"
            }
            );
        }
    }
}
