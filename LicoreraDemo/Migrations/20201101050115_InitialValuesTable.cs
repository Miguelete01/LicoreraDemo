using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LicoreraDemo.Migrations
{
    public partial class InitialValuesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "Apellidos", "Direccion", "NoIdentificacion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Perez", "El Tanque Rojo", "0010101010009J", "Pepe", "89889988" },
                    { 2, "Leon", "Tokyi", "2210101010009J", "Mike", "88776688" },
                    { 3, "Musk", "New York", "0010112010009J", "Elon", "87898786" }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "Id", "Activo", "Descripcion", "Nombre", "PrecioUS", "PreciosNio", "Stock" },
                values: new object[,]
                {
                    { 1, false, "Dell inalambrico", "Mouse", 10m, 210m, 90 },
                    { 2, false, "Dell Mecanico", "Teclado", 25m, 800m, 20 },
                    { 3, false, "Cannon Semi Profecional", "Camara", 100m, 3210m, 14 },
                    { 4, false, "Ortopedica", "Cama", 230m, 8110m, 80 },
                    { 5, false, "Madera de cedro macho", "Mesa", 130m, 4510m, 50 }
                });

            migrationBuilder.InsertData(
                table: "TasaCambio",
                columns: new[] { "Id", "Equivalencia", "Fecha" },
                values: new object[] { 1, 35m, new DateTime(2020, 11, 1, 5, 1, 14, 683, DateTimeKind.Utc).AddTicks(9947) });

            migrationBuilder.InsertData(
                table: "Factura",
                columns: new[] { "Id", "ClienteId", "Fecha", "NoFactura" },
                values: new object[] { 1, 1, new DateTime(2020, 11, 1, 5, 1, 14, 684, DateTimeKind.Utc).AddTicks(4935), "F0001" });

            migrationBuilder.InsertData(
                table: "Factura",
                columns: new[] { "Id", "ClienteId", "Fecha", "NoFactura" },
                values: new object[] { 2, 2, new DateTime(2020, 11, 1, 5, 1, 14, 684, DateTimeKind.Utc).AddTicks(5982), "F0002" });

            migrationBuilder.InsertData(
                table: "DetalleFactura",
                columns: new[] { "Id", "Cantidad", "FacturaId", "ProductoId", "TasaCambioId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1 },
                    { 2, 1, 1, 3, 1 },
                    { 3, 2, 1, 1, 1 },
                    { 4, 2, 2, 4, 1 },
                    { 5, 2, 2, 2, 1 },
                    { 6, 1, 2, 3, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TasaCambio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
