using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class addstockQuantitytoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b251a2d6-7653-4b92-8643-78db0f0aa278", "AQAAAAIAAYagAAAAELRdfx/J3jOJEilG5XnNn7LW5A+cVRMEsccUO1dtRYlDjm6sjy5Q9sQxjsJ1ssIbmQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fab63aa6-6ca6-4583-92ed-b779ec50cc97", "AQAAAAIAAYagAAAAEPKDPpwRFqtolIYrbjb2zZ1AHqg6UIcdU99oTPcqy4tJQvMz+6D2bdntX5ZWkbO8Vw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a998c3c-e794-4c15-931f-e0ed427cbf41", "AQAAAAIAAYagAAAAEJug6CnIciSmx934CQuI0guwEdyB6SGgQefpHBoyD8I4WkuJJ9AA1vg0hqcHcjKeEQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fe7e85e6-706e-425e-a7e6-45149f30bce1", "AQAAAAIAAYagAAAAEDP1QD50ZD5Zdb+mD6/gkfeUy3ufq0xoGIQSIq5UJ89Fde63jCm79yQzUMKi/Mxsjw==" });
        }
    }
}
