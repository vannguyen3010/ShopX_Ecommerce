using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e44d24cb-4201-46ca-b1b2-a558042675b0", "AQAAAAIAAYagAAAAEBUW5ETRF0TiulS5Mvwaf15PEEaeyHtqEBdnpRqUmWB9gBuBGTkxxzhiwL6tNKzTlA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "deae12d8-c11f-4931-8ddb-d58725c2848d", "AQAAAAIAAYagAAAAEFRIuNg95RWNVIOhOoHpkXfDkFaEgy5g2w2Vp9LlRX6nIWZzRi2zEQTR8NS6sSiFHw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fdb89a2-6d1a-4aed-b0a9-14ee3f4e2f0f", "AQAAAAIAAYagAAAAEDbIPVaitCKxRftTXFv+DYhwJu9N+UoA67+Sj2qo++L96W7Mbwk6SwB+qJY6Ml3DqQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7ece7e95-78ec-440c-9dc5-fcc67adce6cd", "AQAAAAIAAYagAAAAEB+RjoMQc6AvdKLyEH+9G9tv9gAFP/zkdJ/w6e4JHxRRaRjBX7e8IuxD92enO+lGlg==" });
        }
    }
}
