using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurante_API.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Estados_Mesas",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Cliente Esperando Pedido" },
                    { 2, "Cliente Comiendo" },
                    { 3, "Cliente Pagando" },
                    { 4, "Cerrada" }
                });

            migrationBuilder.InsertData(
                table: "Estados_Pedidos",
                columns: new[] { "Id", "Descripcion" },
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
                columns: new[] { "Id", "Descripcion" },
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
                columns: new[] { "Id", "Descripcion" },
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
                columns: new[] { "Id", "EmpleadoActivo", "Nombre", "Password", "RolId", "SectorId", "Usuario" },
                values: new object[,]
                {
                    { 1, true, "Juan Pérez", "bartender1", 1, 1, "bartender1" },
                    { 2, true, "María Gómez", "cervecero1", 2, 2, "cervecero1" },
                    { 3, true, "Carlos López", "cocinero1", 3, 3, "cocinero1" },
                    { 4, true, "Ana Martínez", "mozo1", 4, 4, "mozo1" },
                    { 5, true, "Jorge García", "socio1", 5, 5, "socio1" },
                    { 6, false, "Laura Torres", "bartender2", 1, 1, "bartender2" },
                    { 7, true, "Esteban Rodriguez", "socio2", 5, 5, "socio2" },
                    { 8, true, "Pedro Ramirez", "cocinero2", 3, 3, "cocinero2" },
                    { 9, false, "Gonzalo Fernandez", "socio3", 5, 5, "socio3" }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "Codigo", "EstadoMesaId" },
                values: new object[,]
                {
                    { 1, "KA8D2", 1 },
                    { 2, "VTOPN", 1 },
                    { 3, "72CER", 4 },
                    { 4, "2FFV0", 1 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "NombreDesc", "Precio", "SectorId", "Stock" },
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
                columns: new[] { "Id", "MesaId", "NombreCliente" },
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
                columns: new[] { "Id", "EmpleadoLogId", "FechaDeslogueo", "FechaLogueo" },
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
                columns: new[] { "Id", "Cantidad", "CodigoCliente", "ComandaId", "EstadoPedidoId", "FechaCreacion", "FechaFinalizacion", "Observaciones", "ProductoId", "TiempoEstimado" },
                values: new object[,]
                {
                    { 1, 1, "KSPO7", 1, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "Con hielo", 1, 10 },
                    { 2, 2, "NMKN5", 1, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 3, 20 },
                    { 3, 3, "3RIM0", 1, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 3, 30 },
                    { 4, 2, "4O35J", 2, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 8, 15 },
                    { 5, 2, "UT98J", 3, 1, new DateTime(2024, 8, 12, 19, 30, 0, 0, DateTimeKind.Local), null, "", 9, 40 },
                    { 6, 4, "WV5VZ", 5, 4, new DateTime(2024, 8, 24, 19, 30, 0, 0, DateTimeKind.Local), new DateTime(2024, 8, 24, 19, 47, 0, 0, DateTimeKind.Local), "bien frío", 2, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LogueosEmpleados",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados_Pedidos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sectores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados_Mesas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
