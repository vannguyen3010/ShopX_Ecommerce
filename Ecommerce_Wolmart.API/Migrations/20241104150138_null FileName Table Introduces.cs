using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class nullFileNameTableIntroduces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSizeInBytes",
                table: "Introduces",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Introduces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                table: "Introduces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "058b684c-bcad-488e-9382-0924f81859fc", "AQAAAAIAAYagAAAAENZFsDVOwwlVSxfmKuyCIfNxoehtQWoDiX6Md53ImvUsp+6DhwawJzbzdNPczX94Wg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "53788e47-42e9-4a06-a63a-763eb0c62ef5", "AQAAAAIAAYagAAAAELAUWfDT3T1XTZqz03P5/CMY1B5q1LteFFsYmAPkDp4Zfid8wJkDb1+xRP/ZTB/37A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSizeInBytes",
                table: "Introduces",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Introduces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                table: "Introduces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
