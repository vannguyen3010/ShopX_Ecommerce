using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfileUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileUsers_AspNetUsers_UserId1",
                table: "ProfileUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProfileUsers_UserId1",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProfileUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfileUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fdb89a2-6d1a-4aed-b0a9-14ee3f4e2f0f", "AQAAAAIAAYagAAAAEDbIPVaitCKxRftTXFv+DYhwJu9N+UoA67+Sj2qo++L96W7Mbwk6SwB+qJY6Ml3DqQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7ece7e95-78ec-440c-9dc5-fcc67adce6cd", "AQAAAAIAAYagAAAAEB+RjoMQc6AvdKLyEH+9G9tv9gAFP/zkdJ/w6e4JHxRRaRjBX7e8IuxD92enO+lGlg==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_UserId",
                table: "ProfileUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUsers_AspNetUsers_UserId",
                table: "ProfileUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileUsers_AspNetUsers_UserId",
                table: "ProfileUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProfileUsers_UserId",
                table: "ProfileUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProfileUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ProfileUsers",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "IX_ProfileUsers_UserId1",
                table: "ProfileUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileUsers_AspNetUsers_UserId1",
                table: "ProfileUsers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
