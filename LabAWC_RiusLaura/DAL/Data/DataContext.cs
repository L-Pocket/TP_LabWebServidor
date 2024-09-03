using Entidades;
using LabAWC_RiusLaura.DAL.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace LabAWC_RiusLaura.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }

        // DataSeed para insertar registros en la BBDD. Ir descomentando de a uno para migrar.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolSeed());
            modelBuilder.ApplyConfiguration(new SectorSeed());
            modelBuilder.ApplyConfiguration(new EstadoMesaSeed());
            modelBuilder.ApplyConfiguration(new EstadoPedidoSeed());


            modelBuilder.ApplyConfiguration(new ComandaSeed());
            modelBuilder.ApplyConfiguration(new EmpleadoSeed());
            modelBuilder.ApplyConfiguration(new MesaSeed());
            modelBuilder.ApplyConfiguration(new PedidoSeed());
            modelBuilder.ApplyConfiguration(new ProductoSeed());
            modelBuilder.ApplyConfiguration(new LogueoEmpleadoSeed());

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        // Tablas:
        public virtual DbSet<Comanda> Comandas { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<EstadoMesa> Estados_Mesas { get; set; }
        public virtual DbSet<EstadoPedido> Estados_Pedidos { get; set; }
        public virtual DbSet<Mesa> Mesas { get; set; }        
        public virtual DbSet<Pedido> Pedidos { get; set; }        
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Sector> Sectores { get; set; }
        public virtual DbSet<LogueoEmpleado> LogueosEmpleados { get; set; }


    }
}
