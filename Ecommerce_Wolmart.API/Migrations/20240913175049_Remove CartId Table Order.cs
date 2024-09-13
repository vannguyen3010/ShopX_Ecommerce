using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCartIdTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aaf71456-e556-4906-a671-3e8bad8b4920", "AQAAAAIAAYagAAAAEIqGYImbifqUyE2W5ZAa/jiS5IeKLAd523+YjCFircdOrq1a6rZiZorUgYPIAHNi9g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0404c8f1-c092-4af2-916d-0f272ef717d9", "AQAAAAIAAYagAAAAEE6ripwLXhRGNgpOirqAyJDq1dgZn6+YrYElmNqaGWDRBCDiv+yvHRNOCAO3bcOl3w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5cc03372-450a-4150-aa02-575edaad7cf7", "AQAAAAIAAYagAAAAEPXF3vLgdCv7tstMwUdNoYJkGy2ILJZtaFqqHlC4wj8ZRfDgO8fUkWF/4S60+Bhssg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac419d01-6cb0-4913-a8a6-0ad24e0cbc7e", "AQAAAAIAAYagAAAAEHtCqosDZEoMd6v17e3Bg3/wesMfx8lKO5FWK8rAyxPqVyo8oLOmigkLVhOLTRZ33w==" });
        }
    }
}
