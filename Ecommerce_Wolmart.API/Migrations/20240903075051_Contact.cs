using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class Contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f6a235a8-f460-4b1c-a1f2-bafa110b3906", "AQAAAAIAAYagAAAAEEtX6kSD/TZSGR2YyuXllDNh9XrSsuFjdVMJPitPyX4t+JwfaCfc6lzEFdeIODDsmA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fad9518-db1c-42da-a26b-f59c53353223", "AQAAAAIAAYagAAAAEG18JkfKWwGc67Hv9V9ft1YCkG4ZxQ2elnTJJ2M32mbuO90VK51pNvOiRxb6fYRnWw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "34f2940f-3be0-4afb-8c2e-31d7782eb742", "AQAAAAIAAYagAAAAEG2GTxqF8Z5OgBmfxpeKkG3zQWpouSTlM1yRz8w8RYO3ZgD7wp48cm6k+V5HUICAaA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b85c0ae9-e302-4cfa-9df1-da78a7e6774f", "AQAAAAIAAYagAAAAEJDPWyoX/LVg7gL+w7QlvJU2V33sWiXxXxWW9ieXR1e3hzmjp/Xk3+QNiEZa6xV1gQ==" });
        }
    }
}
