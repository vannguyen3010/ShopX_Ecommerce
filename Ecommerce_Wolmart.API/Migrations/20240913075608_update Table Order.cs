using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class updateTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingCosts_ShippingCostId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingCostId",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5cc03372-450a-4150-aa02-575edaad7cf7", "AQAAAAIAAYagAAAAEPXF3vLgdCv7tstMwUdNoYJkGy2ILJZtaFqqHlC4wj8ZRfDgO8fUkWF/4S60+Bhssg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac419d01-6cb0-4913-a8a6-0ad24e0cbc7e", "AQAAAAIAAYagAAAAEHtCqosDZEoMd6v17e3Bg3/wesMfx8lKO5FWK8rAyxPqVyo8oLOmigkLVhOLTRZ33w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fdea07cb-d23f-4fa0-a824-07f26cbd6559", "AQAAAAIAAYagAAAAEFSDGwLX1H1GfA9KF5QOb8cIZVvMxpu/9MgqcQHfTb2QsXuif2f1koawexAmW6z2EQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58818e80-f444-42a5-b0df-6ee60cf0dd7a", "AQAAAAIAAYagAAAAEPyqS0qmqbnqEPg7WygfWKKQdDocsLkFc066kgMB6Tq2Gz7jLMweKPmnqB0hm6dg3g==" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingCostId",
                table: "Orders",
                column: "ShippingCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingCosts_ShippingCostId",
                table: "Orders",
                column: "ShippingCostId",
                principalTable: "ShippingCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
