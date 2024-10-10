using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTableIntroducerelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Introduces_CategoryIntroduces_CategoryIntroduceId",
                table: "Introduces");

            migrationBuilder.DropIndex(
                name: "IX_Introduces_CategoryIntroduceId",
                table: "Introduces");

            migrationBuilder.DropColumn(
                name: "CategoryIntroduceId",
                table: "Introduces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d75ee687-1565-4a65-8907-e51425538c62", "AQAAAAIAAYagAAAAEEvIsp8NDyYDP+opFlyiNY+LJrokiAYgbAlEAEXw0vCaQl1Y5cur9Pb3C+xDCS02Ww==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3676f134-3ed8-45d9-8ab7-5ebc32fc915f", "AQAAAAIAAYagAAAAEJL9YeObUWI59DbKIUNNh/XJ7liTkjtAHGjsBoSctzvPmhjjgv4PvO5ldWq0XfWBKw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Introduces_CategoryId",
                table: "Introduces",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Introduces_CategoryIntroduces_CategoryId",
                table: "Introduces",
                column: "CategoryId",
                principalTable: "CategoryIntroduces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Introduces_CategoryIntroduces_CategoryId",
                table: "Introduces");

            migrationBuilder.DropIndex(
                name: "IX_Introduces_CategoryId",
                table: "Introduces");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryIntroduceId",
                table: "Introduces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62cf3a54-4362-4be0-bb15-9e68157d2711", "AQAAAAIAAYagAAAAELhzFFavdJlPPL5FGRomMC3DHmvwR0WiMPoupk8CycP9WWgWORGkgeFyk3MA65sAVQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e98cc422-fd7a-4344-8d4f-9a896a159873", "AQAAAAIAAYagAAAAEJF+FJbplqCO4uhfg4ZBemP5oBbcPH0EhAu48aeafkftXILwIZkbU9x1ltM8PmpOVA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Introduces_CategoryIntroduceId",
                table: "Introduces",
                column: "CategoryIntroduceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Introduces_CategoryIntroduces_CategoryIntroduceId",
                table: "Introduces",
                column: "CategoryIntroduceId",
                principalTable: "CategoryIntroduces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
