using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddcategoryNameProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Introduces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Introduces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d8b01b20-dfbf-4db4-857d-7742fbff40da", "AQAAAAIAAYagAAAAEOSi7S1GRWVcOZ4Iql9q1+45v4nJfKvHr0Q+sTvO9crBHDbn87RmR7HHxC20QC0GEw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e431128-8d20-48e6-a9d1-78bb834353ab", "AQAAAAIAAYagAAAAEFdZ6aWtwWkA5oetZYgkCO0FzaIRDXLb/VQDTy9Tly5ncl2hqWcwgGzg6+5Vveib8g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Introduces");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Introduces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6612264-2dbf-4381-a8c4-9eaac725c6d4", "AQAAAAIAAYagAAAAELGoZF0WRydaWkoPu7eP8nQctYmUiIBaAg1Q4y0jjBnEhJo5BDpqMwSkcoFa2IeySg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c56f0bd5-7058-46d8-b921-baa2a1da0263", "AQAAAAIAAYagAAAAEBhfhyBRJPd6snQHwr5HZz0JGdgYeUa7nf27doe9nz3AuQk0C5M45PdTyU1z6H8L3w==" });
        }
    }
}
