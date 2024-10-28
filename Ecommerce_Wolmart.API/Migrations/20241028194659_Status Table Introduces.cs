using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class StatusTableIntroduces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Introduces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9d32acb-08d1-49b3-a543-150b8ac2ad59", "AQAAAAIAAYagAAAAEHtn+Al61zvhLiCXfpTSzAHyyk1KiC/Ppb+9iSe6HmyxaAi5z5uAvb5/2+rU5eahiA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f441539f-65bb-4adb-9aa9-0ab4ba0affd4", "AQAAAAIAAYagAAAAEGBhGhKmqj1YskbcE0XdjAqxH9o3K4thqNgePJlE+ZnmiYGJSNB15h5KwpzJlPMQ3w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Introduces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "69cd3fe2-f5ad-49df-864e-be117516b0d8", "AQAAAAIAAYagAAAAEOVUq4aeAA5qmSLNSKkVxxTkDkffUc3r5FkWXww8R9+QjSPeoiTgvnBKCPqS0GByZA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fb8c8e0-1783-4722-a996-5b0bbe095a86", "AQAAAAIAAYagAAAAEPpQkzLXu1SZZU9nLU+YLOJwTsO1NIEzOpcy7G5Lu53VLKFjVC+h/YPIqNSUXDCv/w==" });
        }
    }
}
