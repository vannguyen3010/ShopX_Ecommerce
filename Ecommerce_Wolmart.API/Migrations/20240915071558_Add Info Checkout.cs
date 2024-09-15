using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInfoCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Checkouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Checkouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Checkouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Checkouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Checkouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Checkouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a6e48c1-f103-43a2-8f32-7be5a7643033", "AQAAAAIAAYagAAAAEELqOLvC5LKSX7W/yCf8ZmRs/L4citU5/mKFThW0UbIDmuGJTS/HsANADKg10BdrAA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b7fe9de0-e2af-4ea7-a599-f5545b2ab9cc", "AQAAAAIAAYagAAAAEJ0bcjuFowOCPNgFV4qla6FAYC+S/NjOuTeLpyBQcRhps1CbwJiChaaM2EIMF7zRrA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Checkouts");

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
    }
}
