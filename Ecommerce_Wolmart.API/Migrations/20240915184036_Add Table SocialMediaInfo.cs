using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSocialMediaInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialMediaInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZaloLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TikTokLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaInfos", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a998c3c-e794-4c15-931f-e0ed427cbf41", "AQAAAAIAAYagAAAAEJug6CnIciSmx934CQuI0guwEdyB6SGgQefpHBoyD8I4WkuJJ9AA1vg0hqcHcjKeEQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fe7e85e6-706e-425e-a7e6-45149f30bce1", "AQAAAAIAAYagAAAAEDP1QD50ZD5Zdb+mD6/gkfeUy3ufq0xoGIQSIq5UJ89Fde63jCm79yQzUMKi/Mxsjw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMediaInfos");

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
    }
}
