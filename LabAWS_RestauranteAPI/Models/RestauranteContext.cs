using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

public partial class RestauranteContext : DbContext
{
    public RestauranteContext()
    {
    }

    public RestauranteContext(DbContextOptions<RestauranteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comanda> Comandas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadosMesa> EstadosMesas { get; set; }

    public virtual DbSet<EstadosPedido> EstadosPedidos { get; set; }

    public virtual DbSet<Logueo> Logueos { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sectore> Sectores { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comanda>(entity =>
        {
            entity.HasKey(e => e.IdComanda).HasName("PK__comandas__6D6D170D017BDF6A");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Comanda).HasConstraintName("FK__comandas__id_mes__33D4B598");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__empleado__88B51394D613DF56");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Empleados).HasConstraintName("FK__empleados__id_ro__29572725");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Empleados).HasConstraintName("FK__empleados__id_se__286302EC");
        });

        modelBuilder.Entity<EstadosMesa>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__estados___86989FB2D8897B3F");
        });

        modelBuilder.Entity<EstadosPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__estados___86989FB2921924AE");
        });

        modelBuilder.Entity<Logueo>(entity =>
        {
            entity.HasKey(e => e.IdLogueo).HasName("PK__logueos__0894B2DD0D970A6D");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Logueos).HasConstraintName("FK__logueos__id_empl__47DBAE45");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("PK__mesas__68A1E159CF795B7D");

            entity.Property(e => e.Nombre).IsFixedLength();

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Mesas).HasConstraintName("FK__mesas__id_estado__30F848ED");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__pedidos__6FF0148965FCAA2C");

            entity.Property(e => e.CodigoCliente).IsFixedLength();

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.Pedidos).HasConstraintName("FK__pedidos__id_coma__38996AB5");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Pedidos).HasConstraintName("FK__pedidos__id_esta__3A81B327");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidos).HasConstraintName("FK__pedidos__id_prod__398D8EEE");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__producto__FF341C0D376EAB6D");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Productos).HasConstraintName("FK__productos__id_se__2C3393D0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__roles__6ABCB5E001FC8123");
        });

        modelBuilder.Entity<Sectore>(entity =>
        {
            entity.HasKey(e => e.IdSector).HasName("PK__sectores__3483C3696E448137");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
