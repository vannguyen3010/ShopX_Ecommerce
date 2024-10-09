using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class removeTotalCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCount",
                table: "CategoryIntroduces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35a277db-6ec2-4c0c-b738-fdf6ea26c489", "AQAAAAIAAYagAAAAEDEur95sagr9m0bDo4X6wE4BST5DTySbvqvyGuMnSaJJ14ZaP7VZsa8j3/vqM7K3sQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "33330215-c118-4c9f-bd3c-b53aa52b43ca", "AQAAAAIAAYagAAAAEPnttHVYExAw6ObN+OhDVNtE9YOiHy/oXbWia1aQ+/d+38/wN115lNvIyjpwYsJaPw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCount",
                table: "CategoryIntroduces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "da7677b4-3ee5-4141-a228-e36952292675", "AQAAAAIAAYagAAAAEMUZakUA8ck6klYV5kVvf8UP0/NDyVOKdUUJN6nspsI8TzG/wkcACWJJmi5RAhQHBQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1482d0a-acaa-4def-83dd-0c9c4f54761a", "AQAAAAIAAYagAAAAEN819eF7noGOwZSkzVvs6mhCMbGqB6Q0NBAjVWlcBoY6g8w8RjoRTWoeVZM6qLIm8A==" });
        }
    }
}
