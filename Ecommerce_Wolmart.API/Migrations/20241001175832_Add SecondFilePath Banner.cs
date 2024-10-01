using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondFilePathBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondFileExtension",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondFileName",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondFilePath",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SecondFileSizeInBytes",
                table: "Banners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "326ca363-841f-4c7f-88a9-78e9c956bf11", "AQAAAAIAAYagAAAAEHhKEo/yH9fNXdJ4ULg3bnvTDqGbrm8IcIxcNYLcaR7QN6wsVXewb3/0AK7nZFUxgw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63eb7c9f-4ed4-4319-b3ad-56a6062459ee", "AQAAAAIAAYagAAAAEE/74fQwT26P9Qjl6gvEhwsi7Ge8BgwYcymyVQ55EmdHHXp583H/aB0lMvJBlmoTJw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondFileExtension",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "SecondFileName",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "SecondFilePath",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "SecondFileSizeInBytes",
                table: "Banners");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a1da9e60-ca81-41c8-8f3a-faf2a1ade383", "AQAAAAIAAYagAAAAEHxM65U4UpnZ1AZmUyJszYJ2SuNisVm5KGoE2tU+7pRk8+LqIkhlPmpvLPRE22j4Rg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "716dbbbe-4372-4b8b-961f-94048550a090", "AQAAAAIAAYagAAAAEAbLxVoeKUm8ALy+AqseVLHFe90iZdRdFCktm5asOJIm9Z/77SXhVGyXCzV6ETKoRQ==" });
        }
    }
}
