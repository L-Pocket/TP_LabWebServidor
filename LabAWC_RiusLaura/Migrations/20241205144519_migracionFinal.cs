using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante_API.Migrations
{
    /// <inheritdoc />
    public partial class migracionFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Codigo",
                value: "URVTG");

            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Codigo",
                value: "40MMY");

            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Codigo",
                value: "8O330");

            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 4,
                column: "Codigo",
                value: "EKI9D");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CodigoCliente",
                value: "6XT3S");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CodigoCliente",
                value: "CD1JM");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CodigoCliente",
                value: "3WANA");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CodigoCliente",
                value: "P91CI");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CodigoCliente",
                value: "G5OHV");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CodigoCliente",
                value: "R2TTZ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Codigo",
                value: "KA8D2");

            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Codigo",
                value: "VTOPN");

            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Codigo",
                value: "72CER");

            migrationBuilder.UpdateData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 4,
                column: "Codigo",
                value: "2FFV0");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CodigoCliente",
                value: "KSPO7");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CodigoCliente",
                value: "NMKN5");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CodigoCliente",
                value: "3RIM0");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CodigoCliente",
                value: "4O35J");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CodigoCliente",
                value: "UT98J");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CodigoCliente",
                value: "WV5VZ");
        }
    }
}
