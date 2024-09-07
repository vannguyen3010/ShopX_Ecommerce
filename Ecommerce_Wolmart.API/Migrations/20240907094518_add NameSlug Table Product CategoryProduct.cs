using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class addNameSlugTableProductCategoryProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameSlug",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameSlug",
                table: "CateProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "12264a8d-6b3d-495e-9640-83f55cd3e99e", "AQAAAAIAAYagAAAAEPqoaqWTI3eNTQgIQ+KMIQSDT5J2DTDUM7OTI2+aMypmNKewgQRht+1hfeF/zUmAng==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1cc94611-d826-4438-9fe2-04b5fbf057af", "AQAAAAIAAYagAAAAEHagtaFEiY5xwa6IO64cZcNAz2dY7P2hJWfRUPUxuoIxDk009NNC64IjaiLkOpWtQg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameSlug",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NameSlug",
                table: "CateProducts");

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
    }
}
