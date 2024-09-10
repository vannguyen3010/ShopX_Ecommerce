using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca11631f-40ab-4193-8bf9-64f1bb42cb87", "AQAAAAIAAYagAAAAEN8tdUK44AetqaKevYFaY1TwtjGZqudrbsn6hUAHwu74vn4QENoyMiIKiwkY5wbtew==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66970d99-cf0b-43e5-8b15-5c58dfc42573", "AQAAAAIAAYagAAAAEO7QK7vyComHVTUXe1D6zY1JE2AOONS3SSQNcousji4eyOKS4p8gLyRVUyOBcFKXwg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a727eb3-3ae2-491e-b76b-eb491ef07ded", "AQAAAAIAAYagAAAAEMHqmV28IELwRUe/opIugoIsV9fZg4GL4bSXLs3M/1wUadq/exPq2bPNQGmouAZEzA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc0d1c48-a243-4d43-a91a-35aa0f118754", "AQAAAAIAAYagAAAAEDFRLm/YBli8USd6Rsp7b+/GRbHm6iczC79BKjoUTzZe/Vu+ja9W3up9QDzbOVJ9RQ==" });
        }
    }
}
