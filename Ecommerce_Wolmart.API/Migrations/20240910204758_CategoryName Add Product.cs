using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryNameAddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e871f135-7bcc-4b50-9766-65046eddf4c2", "AQAAAAIAAYagAAAAELHMkG2OS2KP1nuJ0vINwPJB+ZhkO9cPTbJrBP5LFLkch2G5xHe+USmiwWhKKhAzUQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b63bfc88-fa99-406c-a4c4-d9c8e5d5e770", "AQAAAAIAAYagAAAAEF/Wu3aNH1uDOIoqwFg7ImmTzJdbFWHym0H4vAC07opYxK+i3Z0TPezfC/1Ze2vWmQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca11631f-40ab-4193-8bf9-64f1bb42cb87", "AQAAAAIAAYagAAAAEN8tdUK44AetqaKevYFaY1TwtjGZqudrbsn6hUAHwu74vn4QENoyMiIKiwkY5wbtew==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66970d99-cf0b-43e5-8b15-5c58dfc42573", "AQAAAAIAAYagAAAAEO7QK7vyComHVTUXe1D6zY1JE2AOONS3SSQNcousji4eyOKS4p8gLyRVUyOBcFKXwg==" });
        }
    }
}
