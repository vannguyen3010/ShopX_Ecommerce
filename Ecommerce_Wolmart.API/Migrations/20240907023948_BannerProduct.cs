using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class BannerProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerProducts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d59c2c6b-c631-4aa1-ae13-323fcbc5a651", "AQAAAAIAAYagAAAAECW0KkfiYetGH0kgiSpX69OM2pnYrQWXIWUbaxVyT1yWxdSU8sJuepENca523Cly7w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac78f40a-ca21-4771-aa97-c67c9043f5ed", "AQAAAAIAAYagAAAAEFflzOwdk8HTKhT8ijQ87XAxBbyjD6KveQdrTAE/5sXzipU5BkPiM8kRG544VqHiZQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "938a1a25-4acd-4aec-b3cc-486b4dc58f40", "AQAAAAIAAYagAAAAEPhS9tTdDXT7wTjRr5gZLoU9UVUfdAIIuRVQnOBPNN/fyL8XIaJ3Cq2kNCIC0iPFug==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63f62264-2e1c-4970-8f82-310d678432c5", "AQAAAAIAAYagAAAAEGkwUADutPlx44/ZaGLq6AXLFrahyu+3EZqPXN4yMtY3gds11KmIy3K4jIjbRe2QIQ==" });
        }
    }
}
