using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titlte",
                table: "Introduces",
                newName: "titleSlug");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Introduces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae2db30e-05c9-4248-b17f-105a2ce7c56b", "AQAAAAIAAYagAAAAEEN9vmanvqMcTzXjXYoweAAau+h0AjmlwHV3AEfqY3W7P+j+Lr4t8iC95kF/74/ckw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f056a3bf-2e4b-4a31-ac16-255b6eda2664", "AQAAAAIAAYagAAAAEINeW+Xy8uV5GYmj4vryWR3mPXcSVwNKarY4zU32ZDKVd7rhkdCSKLacNMBbbtDb1g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "Introduces");

            migrationBuilder.RenameColumn(
                name: "titleSlug",
                table: "Introduces",
                newName: "Titlte");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8f598eaa-0d6f-4625-a708-6ec06afaa2c6", "AQAAAAIAAYagAAAAEBd8mqolctvVVOLV56JPJda1uX1ln/ERnme34drsj9RKjpSVdvJyjIwEuj1YcDDP5g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9ce4205e-3df2-4a51-8ba6-ed9c7c93d153", "AQAAAAIAAYagAAAAEAGqEYRt0tIjzRnMYfKqb8ru+ok9hlq6O3O5SlWQNRFSenQ6FsYAbbdfzgdKLFDY/g==" });
        }
    }
}
