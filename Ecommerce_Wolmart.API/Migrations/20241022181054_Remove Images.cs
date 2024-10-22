using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileUsers_Images_ImageId",
                table: "ProfileUsers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_ProfileUsers_ImageId",
                table: "ProfileUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a1bf44d9-e45a-438c-8285-c5ce6022cd2d", "AQAAAAIAAYagAAAAEJi9y4Y24+ukLlJ2IIR79BAP848/WCAxgrATWfNaZbsDG949BRyEdPfj8arVwCxQhA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f32de47c-5dc6-4187-b040-8a5a1432525d", "AQAAAAIAAYagAAAAEN+9miQP5gmbWYMbi39MHzmvam2cLakvht2RkJOpGsCkn/2sOThsSUdSZWsSccrD+w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6eacadce-951c-4e74-9c6f-9327a684e14e", "AQAAAAIAAYagAAAAEGNGTsYpd1EKF8IySOYEssKqRWc5k6sJnbRU7Uh/w0M7pqws6i/ks8dhFJy5+067YQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "38b71b74-536c-43ad-941f-30b33c85cff5", "AQAAAAIAAYagAAAAEAESd3ns9blhfVg/qEB9nzH7+RbKqOISWIafmbpSMwZC6bhZJn5gMmR5Ra5NEuOTqA==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_ImageId",
                table: "ProfileUsers",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUsers_Images_ImageId",
                table: "ProfileUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
