using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class fixNameSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "titleSlug",
                table: "Introduces",
                newName: "NameSlug");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Introduces",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "81343935-400f-47fc-a871-a5eebcb60e78", "AQAAAAIAAYagAAAAEGLWrRdb/JpOKGFv6GbsP//wX0COtg4XOdIQ1BRXKeWfA7+T+vBJ/Af3IoRV3AOhSQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a577e7e9-0331-4775-aefc-9be5e5e14122", "AQAAAAIAAYagAAAAEJ0Zwk87aVYj4j5x7/V3PNBNJl7JUMsSni3chu0am2FpqiK0X799rFd2B20KW63Z1Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameSlug",
                table: "Introduces",
                newName: "titleSlug");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Introduces",
                newName: "title");

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
    }
}
