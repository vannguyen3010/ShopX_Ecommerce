using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class StatusTableCateIntroduce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CategoryIntroduces",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CategoryIntroduces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e7574fa0-07c3-4156-a013-a482dfceb53d", "AQAAAAIAAYagAAAAEMjJO8j08tkvu9920wZLA/9SwFQDJO9PrzFEv27KlBV94pls8h8PFC5dm4LnKg6ZOw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2963430b-7d1e-45e6-9ca3-46810e8fb698", "AQAAAAIAAYagAAAAEOnVUPcHAE/82+bmqmxRBaCVlgBwDQLu43XkXPkaYxqfyfRLkZbZ4aoQLBNuU7BLTA==" });
        }
    }
}
