using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class CommentProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentProducts", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aeaf720f-f86c-4c2d-8644-988359b78532", "AQAAAAIAAYagAAAAEMSFH5T3l+9OVyXYUv+Mp7ytoOxYWjLVsi1I+y3fwYEBEUy3BGLhiSTKLygzDBLiyQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6120e793-32c7-4d0b-a7fd-0e8d7fc329e2", "AQAAAAIAAYagAAAAEHskNIU1QriLw37thf/CU2VnnGHy16p4iDJV8Cz1np4XVN2HR3A9AZCFkal2IR6wXg==" });
        }
    }
}
