using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileDescription",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                table: "ProfileUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TikTokLink",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "ProfileUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ZaloLink",
                table: "ProfileUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "FileDescription",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "TikTokLink",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "ProfileUsers");

            migrationBuilder.DropColumn(
                name: "ZaloLink",
                table: "ProfileUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86c7b325-e68a-4c52-a1f8-71d9e2180d25", "AQAAAAIAAYagAAAAEKJFJOnsjppYCGFCTJV9WdCtTEY2LBnqjlMRcmBSk7iPmEiE6AoJ3AdFQ7cwvAxdeQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e8854d11-59ba-444d-9b29-62d5c64c0a50", "AQAAAAIAAYagAAAAEEbNapb3xCyEvdmPUh7uaGK/dYB4smIJDVlTBQxR0HF7fJMMq1Sjxqv8BlZ3/2vJSQ==" });
        }
    }
}
