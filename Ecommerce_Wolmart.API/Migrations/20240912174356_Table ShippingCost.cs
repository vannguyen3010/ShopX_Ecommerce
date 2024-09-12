using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class TableShippingCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCosts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0aaadb69-47c8-4d41-988c-6578da4158b9", "AQAAAAIAAYagAAAAEF4yHAWM1E0hCKbKzOztVIN9dPqwAUqFCgNfT083/7mPjg459gS5igXJr0BSyt/Rtg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0fa379c7-c035-4101-b0e7-3e7d59268c3e", "AQAAAAIAAYagAAAAEEoa9pH4623OyaI8CNH01BgipSroObYJO9VdL4+cwTDxnhC78mVhheIkhu+g4cirqw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingCosts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1e98b7a0-638b-4340-9284-49d989563319", "AQAAAAIAAYagAAAAEPuxHEnKiDRx99i2CCLeUj9O0Chrfwx0V6c7vpEkHA9urf/angHWzaW4OgNCtHt8VQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e620b9a7-5f1b-427d-9eb8-21805c2a8cf5", "AQAAAAIAAYagAAAAEIEsp03L59p1SPo2rM/P6hhooPVptFLs3Rzwc4WpQ/KAX9JQrcu/P+qPqK/kEoBt1g==" });
        }
    }
}
