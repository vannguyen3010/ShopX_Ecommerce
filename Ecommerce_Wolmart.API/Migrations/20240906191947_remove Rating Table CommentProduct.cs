using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class removeRatingTableCommentProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CommentProducts");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "938a1a25-4acd-4aec-b3cc-486b4dc58f40", "AQAAAAIAAYagAAAAEPhS9tTdDXT7wTjRr5gZLoU9UVUfdAIIuRVQnOBPNN/fyL8XIaJ3Cq2kNCIC0iPFug==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63f62264-2e1c-4970-8f82-310d678432c5", "AQAAAAIAAYagAAAAEGkwUADutPlx44/ZaGLq6AXLFrahyu+3EZqPXN4yMtY3gds11KmIy3K4jIjbRe2QIQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentProducts_ProductId",
                table: "CommentProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProducts_Products_ProductId",
                table: "CommentProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentProducts_Products_ProductId",
                table: "CommentProducts");

            migrationBuilder.DropIndex(
                name: "IX_CommentProducts_ProductId",
                table: "CommentProducts");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "CommentProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "546ad82a-1eee-42ef-a577-8da581b6eaa8", "AQAAAAIAAYagAAAAEHsVBnZFCYZgphSGMaOePFqwfSoAbchPRlIL+CarnEXGe+s+zbxm6fcGA8hrTAfY4Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84071938-31c9-4544-9182-d78e62103ec1", "AQAAAAIAAYagAAAAEG2OtxMKMfKEGS8TMo1bU59PK/k8XFb2lMTErg95Pb0QKNlFyCmSfj6cr4t1RPOMhQ==" });
        }
    }
}
