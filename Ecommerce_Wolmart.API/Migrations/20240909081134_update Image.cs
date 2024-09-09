using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class updateImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4237e700-42fa-4e5e-8487-55d2670079e6", "AQAAAAIAAYagAAAAEK2Qd//fmMHAa9JbIBGtMi3daMpLnOFnzgPiVzDoxaexJPX3X/GOZ1z1e5iUBEt4oQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5df4802d-bb27-4bf9-a22c-b7f118a16235", "AQAAAAIAAYagAAAAEJyGCQHfH6pvhAf4XVrnP6S88BN8umlYDGsyvKTDOz7++Vldqr9TZgPOmX5HLIyJEw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "83dd4cdd-a662-4402-b6ac-2bb284b45cb3", "AQAAAAIAAYagAAAAENM8DuYJ2O3wOn3DRfc5KyJ/F4rPscxwSlTQp98rLgqjaoU/mivwsLvB8D8ngYvVyQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c59db5f9-d5df-444e-84ec-25314b075583", "AQAAAAIAAYagAAAAEC3YCSESFXNNv1HObH/z1pOgXsW/iEpoU3uuA8HC1na1xzq/HItGsMhCOs5bC2wirw==" });
        }
    }
}
