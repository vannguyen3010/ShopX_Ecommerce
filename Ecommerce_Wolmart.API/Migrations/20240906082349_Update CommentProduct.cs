using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommentProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CommentProducts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CommentProducts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CommentProducts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_CommentProducts_UserId",
                table: "CommentProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProducts_AspNetUsers_UserId",
                table: "CommentProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentProducts_AspNetUsers_UserId",
                table: "CommentProducts");

            migrationBuilder.DropIndex(
                name: "IX_CommentProducts_UserId",
                table: "CommentProducts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CommentProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CommentProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CommentProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0b8f555-2182-4441-b8ca-3523d30b2ab3", "AQAAAAIAAYagAAAAEFd5eAr7dyiGuGnKftJ7b9uKk5kTDEbVKRMM470g8/7pZpsVlq061KVSzIO9WpkoXQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bf979fab-a9d1-4a17-913f-1332bf6f03ea", "AQAAAAIAAYagAAAAECzdXmUYquLHT3TBs9LQgX19Ch89JbfsEAsQRN1iL7vOCVef8KwanpivJastf5OKxA==" });
        }
    }
}
