using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUSerremovelastNameFirstName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "868e190a-a98b-4034-b2dd-1beb591d454b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac6c2dbc-dd4a-4a5d-97ba-3a9db7dd2f62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d16bf389-95ff-4be2-b4a4-bd39db1e093f");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92ebff93-c8f1-43d6-b667-9df2f8a14585", null, new DateTime(2024, 11, 12, 18, 42, 57, 800, DateTimeKind.Utc).AddTicks(4010), "UserRole", "User", "USER" },
                    { "bbfdaab2-3d1b-4618-a91d-5b46f70483fa", null, new DateTime(2024, 11, 12, 18, 42, 57, 800, DateTimeKind.Utc).AddTicks(4006), "UserRole", "Admin", "ADMIN" },
                    { "c2f6d6b4-bf01-48b7-adcb-023ce74362c6", null, new DateTime(2024, 11, 12, 18, 42, 57, 800, DateTimeKind.Utc).AddTicks(3986), "UserRole", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b72a55e-0189-4665-87ab-b8c4a44e00f0",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "5310a746-5fc9-4f7f-b00a-24e45cf14bfa", "superadmin@gmail.com", "SUPERADMIN@GMAIL.COM", "NGUOIDEV", "AQAAAAIAAYagAAAAEBuN9G2CMUPf6Mdg0jaAnbkbsdqAS18lVCryVcpV6pzA1Jl0vMzn2sHfNAqg0N/8CA==", "NguoiDev" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92ebff93-c8f1-43d6-b667-9df2f8a14585");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbfdaab2-3d1b-4618-a91d-5b46f70483fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2f6d6b4-bf01-48b7-adcb-023ce74362c6");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "868e190a-a98b-4034-b2dd-1beb591d454b", null, new DateTime(2024, 11, 12, 17, 49, 21, 393, DateTimeKind.Utc).AddTicks(9917), "UserRole", "SuperAdmin", "SUPERADMIN" },
                    { "ac6c2dbc-dd4a-4a5d-97ba-3a9db7dd2f62", null, new DateTime(2024, 11, 12, 17, 49, 21, 393, DateTimeKind.Utc).AddTicks(9939), "UserRole", "User", "USER" },
                    { "d16bf389-95ff-4be2-b4a4-bd39db1e093f", null, new DateTime(2024, 11, 12, 17, 49, 21, 393, DateTimeKind.Utc).AddTicks(9932), "UserRole", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b72a55e-0189-4665-87ab-b8c4a44e00f0",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "a860d029-2365-48a6-8526-6d73e7fd325c", "superadmin@example.com", "NguyenDev", "Nguoi", "SUPERADMIN@EXAMPLE.COM", "SUPERADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEmY4LwtpgvNh48tT6voQiXKLqPKHHNWEFsiNnyblkn4GKoXc4aRR19T3X2NGV1SgA==", "superadmin@example.com" });
        }
    }
}
