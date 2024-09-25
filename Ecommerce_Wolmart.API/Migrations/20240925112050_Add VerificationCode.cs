using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "VerificationCode" },
                values: new object[] { "a1da9e60-ca81-41c8-8f3a-faf2a1ade383", "AQAAAAIAAYagAAAAEHxM65U4UpnZ1AZmUyJszYJ2SuNisVm5KGoE2tU+7pRk8+LqIkhlPmpvLPRE22j4Rg==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "VerificationCode" },
                values: new object[] { "716dbbbe-4372-4b8b-961f-94048550a090", "AQAAAAIAAYagAAAAEAbLxVoeKUm8ALy+AqseVLHFe90iZdRdFCktm5asOJIm9Z/77SXhVGyXCzV6ETKoRQ==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d705105b-2437-4180-b782-a583cfc1a0a7", "AQAAAAIAAYagAAAAEB5D/Xz75Y8uDdMaKYOx1HF79WbcTckP4pKFEY6zFNaPtppZ4UJ0eUXwoco6V2NAsA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "36ed7b0e-5d59-4b56-9f23-6b650772066d", "AQAAAAIAAYagAAAAED7AtJ08kdTl+Fc7ri4VvM0Mtjw6pyEdt3VJWtdUWUFQLatdTKnhryrniQ9g0+nzIg==" });
        }
    }
}
