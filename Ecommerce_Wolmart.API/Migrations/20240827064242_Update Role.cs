using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a670f0d-a08f-4bba-b1fd-9b6df6e42d70", null, new DateTime(2015, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "UserRole", "User", "USER" },
                    { "cfa9978f-2afd-4786-9cf9-97b4493f4d34", null, new DateTime(2015, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "UserRole", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a2bd32c0-d75e-4966-8274-758e273da3fb", 0, "18bdd8b7-7024-4af6-9135-cc67fe0954ef", "user@example.com", true, "John", "Doe", false, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEG6xwo5PMlSlSVKZeJaqX3aa27fSheM7DrwZwx8KATa4Pc1GLTThUTe93KgKTc7BXA==", null, false, "", false, "user@example.com" },
                    { "d7930984-3648-45c8-b33e-7b902e1166b4", 0, "401f3742-f6e4-469e-b333-0eea5cc51c1f", "user2@example.com", true, "John2", "Doe2", false, null, "USER2@EXAMPLE.COM", "USER2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGE9QBwbvazzQTnr32WN2opPkZwj4EcEkXthtC5asxa0+RSAidB6av6NlXMyCOtbCg==", null, false, "", false, "user2@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cfa9978f-2afd-4786-9cf9-97b4493f4d34", "a2bd32c0-d75e-4966-8274-758e273da3fb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a670f0d-a08f-4bba-b1fd-9b6df6e42d70");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cfa9978f-2afd-4786-9cf9-97b4493f4d34", "a2bd32c0-d75e-4966-8274-758e273da3fb" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfa9978f-2afd-4786-9cf9-97b4493f4d34");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
