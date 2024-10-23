using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFirstNamelastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ProfileUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ProfileUsers",
                newName: "UserName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce7c6943-3e34-4c64-81b3-521fb8bf72fa", "AQAAAAIAAYagAAAAEGFCBCJNUCChfkPATmo1/FCXf+TP8BuNNijqP/sy9YQ/TV68H8uH3LMy/JtAnRy3Xg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e39efbdf-9083-47ec-9381-8a371edf46ca", "AQAAAAIAAYagAAAAECFBkZtNKukkncHGHTldSIq53xJ5C6oKAzS5B7joMoqoJ5RCN73YGF5mHtw9fFyk/w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "ProfileUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "ProfileUsers",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
