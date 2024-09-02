using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class fixBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Images_ImageId",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_ImageId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Banners",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileDescription",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                table: "Banners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Banners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "34f2940f-3be0-4afb-8c2e-31d7782eb742", "AQAAAAIAAYagAAAAEG2GTxqF8Z5OgBmfxpeKkG3zQWpouSTlM1yRz8w8RYO3ZgD7wp48cm6k+V5HUICAaA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b85c0ae9-e302-4cfa-9df1-da78a7e6774f", "AQAAAAIAAYagAAAAEJDPWyoX/LVg7gL+w7QlvJU2V33sWiXxXxWW9ieXR1e3hzmjp/Xk3+QNiEZa6xV1gQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "FileDescription",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Banners",
                newName: "Description");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Banners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ccd08af-7985-43a4-b832-6104962715f2", "AQAAAAIAAYagAAAAEFwuuOszQB13jXJlYo2daFu86rQoNJgq8rSbhPRQdlo0rCiN3z/HnHfqJOWCuApzNw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "940396fc-3c17-4c49-8f13-a91308c94780", "AQAAAAIAAYagAAAAEDYSM8qnftQiPrTpr7+xBMrENUw52FtacYvUv8zZWVykz/j7cQoL4aZWp7EiMyyo/w==" });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_ImageId",
                table: "Banners",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Images_ImageId",
                table: "Banners",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
