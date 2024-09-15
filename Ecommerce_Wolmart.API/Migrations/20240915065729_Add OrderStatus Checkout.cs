using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStatusCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OrderStatus",
                table: "Checkouts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "826a4385-700e-41ce-b61c-e58690d31106", "AQAAAAIAAYagAAAAEKr0GCSVJeJFzHPJc6GXkIxS3Z3FkESyzr+TRfhsBGDFC6RYsukT0FrPQCs/fR5vNw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "81359ac5-8ac4-466a-a5e7-95b5a5e86bfe", "AQAAAAIAAYagAAAAEFS1CiWlucAAvNGhVt5cnsdMg4P6fuE0YPufL68D0tBx1nNBNwsLSF3L++IWul1bGA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Checkouts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3d1acc31-9f26-40ca-84d4-d0c54afe88ba", "AQAAAAIAAYagAAAAEFoUrULiYsxgrJtoIi4PiwQ9V6MpJHBaDltTRn5dXLmqGMjiii5hf20OZ5AgytOP5w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a9ae971a-4383-4fa1-94ff-88d4a4cd0402", "AQAAAAIAAYagAAAAEMva6ULmiQuYc/u01C0YtL0LBLW0CtoDu3P2vezPatI5UAGyheZC60IzaMKU7fMzOw==" });
        }
    }
}
