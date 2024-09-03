using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LabAWS_RiusLaura.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Estados_Mesas",
                columns: new[] { "IdEstadoMesa", "DescripcionMesa" },
                values: new object[,]
                {
                    { 1, "Cliente Esperando Pedido" },
                    { 2, "Cliente Comiendo" },
                    { 3, "Cliente Pagando" },
                    { 4, "Cerrada" }
                });

            migrationBuilder.InsertData(
                table: "Estados_Pedidos",
                columns: new[] { "IdEstadoPedido", "DescripcionPedido" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "En Preparacion" },
                    { 3, "Listo Para Servir" },
                    { 4, "Servido" },
                    { 5, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "DescripcionRol" },
                values: new object[,]
                {
                    { 1, "Bartender" },
                    { 2, "Cervecero" },
                    { 3, "Cocinero" },
                    { 4, "Mozo" },
                    { 5, "Socio" }
                });

            migrationBuilder.InsertData(
                table: "Sectores",
                columns: new[] { "IdSector", "DescripcionSector" },
                values: new object[,]
                {
                    { 1, "Barra Tragos Y Vino" },
                    { 2, "Cerveza Artesanal" },
                    { 3, "Cocina" },
                    { 4, "Candybar" },
                    { 5, "General" }
                });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "IdEmpleado", "EmpleadoActivo", "Nombre", "Password", "RolDelEmpleadoId", "SectorDelEmpleadoId", "Usuario" },
                values: new object[,]
                {
                    { 1, true, "Juan Pérez", "password1", 1, 1, "jperez" },
                    { 2, true, "María Gómez", "password2", 2, 2, "mgomez" },
                    { 3, true, "Carlos López", "password3", 3, 3, "clopez" },
                    { 4, true, "Ana Martínez", "password4", 4, 4, "amartinez" },
                    { 5, true, "Jorge García", "password5", 5, 5, "jgarcia" },
                    { 6, false, "Laura Torres", "password6", 1, 1, "ltorres" },
                    { 7, true, "Esteban Rodriguez", "password7", 5, 5, "erodriguez" },
                    { 8, true, "Pedro Ramirez", "password8", 3, 3, "pramirez" },
                    { 9, false, "Gonzalo Fernandez", "password9", 5, 5, "gfernandez" }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "IdMesa", "CodigoMesa", "EstadoDeMesaId" },
                values: new object[,]
                {
                    { 1, "M1001", 1 },
                    { 2, "M1002", 1 },
                    { 3, "M1003", 4 },
                    { 4, "M1004", 1 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "IdProducto", "NombreDescProducto", "PrecioProducto", "SectorProductoId", "StockProducto" },
                values: new object[,]
                {
                    { 1, "Vino tinto Malbec", 14000.00m, 1, 50 },
                    { 2, "Vino tinto Cabernet", 14000.00m, 1, 40 },
                    { 3, "Cerveza artesanal IPA Roja", 3700.00m, 2, 200 },
                    { 4, "Cerveza artesanal Negra", 3700.00m, 2, 150 },
                    { 5, "Empanadas de Carne", 1500.00m, 3, 200 },
                    { 6, "Empanadas de Verdura", 1500.00m, 3, 100 },
                    { 7, "Empanadas de Pollo", 1500.00m, 3, 150 },
                    { 8, "Postre Tiramisú", 5000.00m, 4, 40 },
                    { 9, "Café", 2500.00m, 4, 400 }
                });

            migrationBuilder.InsertData(
                table: "Comandas",
                columns: new[] { "IdComanda", "MesaDeComandaId", "NombreCliente" },
                values: new object[,]
                {
                    { 1, 1, "Cliente A" },
                    { 2, 2, "Cliente B" },
                    { 3, 3, "Cliente C" },
                    { 4, 4, "Cliente D" },
                    { 5, 1, "Cliente E" }
                });

            migrationBuilder.InsertData(
                table: "LogueosEmpleados",
                columns: new[] { "IdLogueo", "EmpleadoLogId", "FechaDeslogueo", "FechaLogueo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 12, 23, 59, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 12, 19, 6, 0, 0, DateTimeKind.Local) },
                    { 2, 2, new DateTime(2024, 8, 12, 23, 59, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 12, 19, 1, 0, 0, DateTimeKind.Local) },
                    { 3, 3, new DateTime(2024, 8, 12, 23, 49, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 12, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 4, new DateTime(2024, 8, 12, 23, 15, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 12, 18, 16, 0, 0, DateTimeKind.Local) },
                    { 5, 5, new DateTime(2024, 8, 13, 23, 33, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 13, 19, 0, 0, 0, DateTimeKind.Local) },
                    { 6, 6, new DateTime(2024, 8, 13, 23, 55, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 13, 19, 30, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "IdPedido", "Cantidad", "CodigoCliente", "ComandaDelPedidoId", "EstadoDelPedidoId", "FechaCreacion", "FechaFinalizacion", "ObservacionesDelPedido", "ProductoDelPedidoId", "TiempoEstimado" },
                values: new object[,]
                {
                    { 1, 1, "MBC12", 1, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "Con hielo", 1, 10 },
                    { 2, 2, "MBC12", 1, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 3, 20 },
                    { 3, 3, "MBC12", 1, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 3, 30 },
                    { 4, 2, "AD32S", 2, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 8, 15 },
                    { 5, 2, "KAE2K", 3, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 9, 40 },
                    { 6, 4, "TYH3K", 5, 4, new DateTime(2024, 8, 24, 19, 30, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 24, 19, 47, 0, 0, DateTimeKind.Local), "bien frío", 2, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "IdComanda",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "IdEstadoMesa",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "IdEstadoMesa",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "IdEstadoPedido",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "IdEstadoPedido",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "IdEstadoPedido",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "IdLogueo",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "IdLogueo",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "IdLogueo",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "IdLogueo",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "IdLogueo",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "IdLogueo",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "IdPedido",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "IdPedido",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "IdPedido",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "IdPedido",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "IdPedido",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "IdPedido",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "IdComanda",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "IdComanda",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "IdComanda",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "IdComanda",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "IdEmpleado",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "IdEstadoPedido",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "IdEstadoPedido",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "IdMesa",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "IdProducto",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "IdMesa",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "IdMesa",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "IdMesa",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "IdSector",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "IdSector",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "IdSector",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "IdSector",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "IdSector",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "IdEstadoMesa",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "IdEstadoMesa",
                keyValue: 4);
        }
    }
}
