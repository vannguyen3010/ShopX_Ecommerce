using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "CateProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dacf13f1-a59b-48b0-b169-3791fcff739a", "AQAAAAIAAYagAAAAEPCRDBt6dfhk6/GW5CS9iCjeGN3W9A38X9hQXjKTKDkHWdZwjS7BG2NroHnpb7LXXA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e891811c-3747-4d8f-bdfe-1441fa906008", "AQAAAAIAAYagAAAAEAq1wzZDVr+qn5wRMG/71QFWXeRYlab6cLn+z6L83zXloZoanO9v+Jd0wD+nIpeF7g==" });

            migrationBuilder.CreateIndex(
                name: "IX_CateProducts_ParentCategoryId",
                table: "CateProducts",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CateProducts_CateProducts_ParentCategoryId",
                table: "CateProducts",
                column: "ParentCategoryId",
                principalTable: "CateProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CateProducts_CateProducts_ParentCategoryId",
                table: "CateProducts");

            migrationBuilder.DropIndex(
                name: "IX_CateProducts_ParentCategoryId",
                table: "CateProducts");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "CateProducts");

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d99bd4bb-df96-4b9e-94a8-2a70256518d2", "AQAAAAIAAYagAAAAENKQblUIFqUrGZnQiBZzKPtquxFtzc8j0pS2ZCTQKwrbCB12rOgQU+daiwEGqhKXjw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "67d0fead-c3ee-48ff-a795-f2083aacf7e2", "AQAAAAIAAYagAAAAEG8zhixi203IRkhC8Sey6gu09fZQImRs7IJNS7KcCvpyRrgyzD2VanTRTnBhMgZaaA==" });
        }
    }
}
