using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a670f0d-a08f-4bba-b1fd-9b6df6e42d70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b72a55e-0189-4665-87ab-b8c4a44e00f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfa9978f-2afd-4786-9cf9-97b4493f4d34");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a860d029-2365-48a6-8526-6d73e7fd325c", "AQAAAAIAAYagAAAAEEmY4LwtpgvNh48tT6voQiXKLqPKHHNWEFsiNnyblkn4GKoXc4aRR19T3X2NGV1SgA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a670f0d-a08f-4bba-b1fd-9b6df6e42d70", null, new DateTime(2024, 11, 12, 17, 45, 43, 153, DateTimeKind.Utc).AddTicks(6102), "UserRole", "Admin", "ADMIN" },
                    { "7b72a55e-0189-4665-87ab-b8c4a44e00f0", null, new DateTime(2024, 11, 12, 17, 45, 43, 153, DateTimeKind.Utc).AddTicks(6108), "UserRole", "User", "USER" },
                    { "cfa9978f-2afd-4786-9cf9-97b4493f4d34", null, new DateTime(2024, 11, 12, 17, 45, 43, 153, DateTimeKind.Utc).AddTicks(6088), "UserRole", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b72a55e-0189-4665-87ab-b8c4a44e00f0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9efb5228-7295-4927-9fa8-4c4daf90a569", "AQAAAAIAAYagAAAAEMbHU+1SiOUwsbsILGfU6/3F1yu5fB135s4DYgELWhm1yQSq2rr1a3Dplw2Zn3JkYg==" });
        }
    }
}
