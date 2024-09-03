using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabAWS_RiusLaura.Migrations
{
    /// <inheritdoc />
    public partial class InicialTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados_Mesas",
                columns: table => new
                {
                    IdEstadoMesa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionMesa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados_Mesas", x => x.IdEstadoMesa);
                });

            migrationBuilder.CreateTable(
                name: "Estados_Pedidos",
                columns: table => new
                {
                    IdEstadoPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionPedido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados_Pedidos", x => x.IdEstadoPedido);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionRol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    IdSector = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionSector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.IdSector);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    IdMesa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoMesa = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    EstadoDeMesaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.IdMesa);
                    table.ForeignKey(
                        name: "FK_Mesas_Estados_Mesas_EstadoDeMesaId",
                        column: x => x.EstadoDeMesaId,
                        principalTable: "Estados_Mesas",
                        principalColumn: "IdEstadoMesa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SectorDelEmpleadoId = table.Column<int>(type: "int", nullable: false),
                    RolDelEmpleadoId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_RolDelEmpleadoId",
                        column: x => x.RolDelEmpleadoId,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_Sectores_SectorDelEmpleadoId",
                        column: x => x.SectorDelEmpleadoId,
                        principalTable: "Sectores",
                        principalColumn: "IdSector",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorProductoId = table.Column<int>(type: "int", nullable: false),
                    NombreDescProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockProducto = table.Column<int>(type: "int", nullable: false),
                    PrecioProducto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Sectores_SectorProductoId",
                        column: x => x.SectorProductoId,
                        principalTable: "Sectores",
                        principalColumn: "IdSector",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    IdComanda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesaDeComandaId = table.Column<int>(type: "int", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.IdComanda);
                    table.ForeignKey(
                        name: "FK_Comandas_Mesas_MesaDeComandaId",
                        column: x => x.MesaDeComandaId,
                        principalTable: "Mesas",
                        principalColumn: "IdMesa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogueosEmpleados",
                columns: table => new
                {
                    IdLogueo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoLogId = table.Column<int>(type: "int", nullable: false),
                    FechaLogueo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDeslogueo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogueosEmpleados", x => x.IdLogueo);
                    table.ForeignKey(
                        name: "FK_LogueosEmpleados_Empleados_EmpleadoLogId",
                        column: x => x.EmpleadoLogId,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComandaDelPedidoId = table.Column<int>(type: "int", nullable: false),
                    ProductoDelPedidoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    EstadoDelPedidoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TiempoEstimado = table.Column<int>(type: "int", nullable: false),
                    CodigoCliente = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ObservacionesDelPedido = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Comandas_ComandaDelPedidoId",
                        column: x => x.ComandaDelPedidoId,
                        principalTable: "Comandas",
                        principalColumn: "IdComanda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Estados_Pedidos_EstadoDelPedidoId",
                        column: x => x.EstadoDelPedidoId,
                        principalTable: "Estados_Pedidos",
                        principalColumn: "IdEstadoPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Productos_ProductoDelPedidoId",
                        column: x => x.ProductoDelPedidoId,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_MesaDeComandaId",
                table: "Comandas",
                column: "MesaDeComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_RolDelEmpleadoId",
                table: "Empleados",
                column: "RolDelEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_SectorDelEmpleadoId",
                table: "Empleados",
                column: "SectorDelEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_LogueosEmpleados_EmpleadoLogId",
                table: "LogueosEmpleados",
                column: "EmpleadoLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_EstadoDeMesaId",
                table: "Mesas",
                column: "EstadoDeMesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ComandaDelPedidoId",
                table: "Pedidos",
                column: "ComandaDelPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EstadoDelPedidoId",
                table: "Pedidos",
                column: "EstadoDelPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProductoDelPedidoId",
                table: "Pedidos",
                column: "ProductoDelPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_SectorProductoId",
                table: "Productos",
                column: "SectorProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogueosEmpleados");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Comandas");

            migrationBuilder.DropTable(
                name: "Estados_Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropTable(
                name: "Estados_Mesas");
        }
    }
}
