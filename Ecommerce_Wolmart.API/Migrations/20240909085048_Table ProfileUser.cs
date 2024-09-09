using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class TableProfileUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileUsers_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfileUsers_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0b9efc48-eaf4-495d-8152-0980f2e14fe4", "AQAAAAIAAYagAAAAEH4nyqyWzK5tQjVi+tQd593Co7eh8WbjcTWwK9SwpC9o6Yuz7atUtrwVPFmEj91Izw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "db35f27d-b19d-4ff9-b9d8-07950632c9e1", "AQAAAAIAAYagAAAAEL0rvU4kuUTTF01o1p4KMwFQyn6icFH4oTETpqaLSp8JuJuxzbiDB6u3/gTi3+gIxw==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_ImageId",
                table: "ProfileUsers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_UserId1",
                table: "ProfileUsers",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileUsers");

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
    }
}
