using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class JsonIgnoreCateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c2d2bac-06e1-4ca0-bcf1-b04d37846c07", "AQAAAAIAAYagAAAAEDX1AtXpSS8rQEeoZaahawT9476H0O19YgbmEyXgrJNLkYVBfCpH6MRwW91y3tz5nQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1bf4632a-81c5-4296-ab5c-f2c70e775e94", "AQAAAAIAAYagAAAAEFViXWkHgRGewqoFFS5y3jtuOo6ESvJspBTY0GK/Yg9Gs0Fm2qz+xMWRR6NLEmsPVQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
