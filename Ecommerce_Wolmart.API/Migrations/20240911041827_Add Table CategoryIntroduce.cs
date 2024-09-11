using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCategoryIntroduce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryIntroduceId",
                table: "Introduces",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryIntroduces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameSlug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIntroduces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryIntroduces_CategoryIntroduces_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "CategoryIntroduces",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6612264-2dbf-4381-a8c4-9eaac725c6d4", "AQAAAAIAAYagAAAAELGoZF0WRydaWkoPu7eP8nQctYmUiIBaAg1Q4y0jjBnEhJo5BDpqMwSkcoFa2IeySg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c56f0bd5-7058-46d8-b921-baa2a1da0263", "AQAAAAIAAYagAAAAEBhfhyBRJPd6snQHwr5HZz0JGdgYeUa7nf27doe9nz3AuQk0C5M45PdTyU1z6H8L3w==" });

            migrationBuilder.CreateIndex(
                name: "IX_Introduces_CategoryIntroduceId",
                table: "Introduces",
                column: "CategoryIntroduceId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIntroduces_ParentCategoryId",
                table: "CategoryIntroduces",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Introduces_CategoryIntroduces_CategoryIntroduceId",
                table: "Introduces",
                column: "CategoryIntroduceId",
                principalTable: "CategoryIntroduces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Introduces_CategoryIntroduces_CategoryIntroduceId",
                table: "Introduces");

            migrationBuilder.DropTable(
                name: "CategoryIntroduces");

            migrationBuilder.DropIndex(
                name: "IX_Introduces_CategoryIntroduceId",
                table: "Introduces");

            migrationBuilder.DropColumn(
                name: "CategoryIntroduceId",
                table: "Introduces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e871f135-7bcc-4b50-9766-65046eddf4c2", "AQAAAAIAAYagAAAAELHMkG2OS2KP1nuJ0vINwPJB+ZhkO9cPTbJrBP5LFLkch2G5xHe+USmiwWhKKhAzUQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b63bfc88-fa99-406c-a4c4-d9c8e5d5e770", "AQAAAAIAAYagAAAAEF/Wu3aNH1uDOIoqwFg7ImmTzJdbFWHym0H4vAC07opYxK+i3Z0TPezfC/1Ze2vWmQ==" });
        }
    }
}
