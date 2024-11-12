using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class addemailaddressSocialMediaInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "SocialMediaInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SocialMediaInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c8929236-6306-44d8-8642-f992a4d2bc4a", "AQAAAAIAAYagAAAAEHjGmLocsT+l8+bEcX7wk887pkVudzjV2kNpg+C6KNOTBJ08w6nxoaxlGyldv1ScPw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f924194-9bcc-4bf4-a4de-a19db9f30add", "AQAAAAIAAYagAAAAEK0kRsnepHEOttDUxV2Esvp8M69JeRcOJenPlr5oHBcPRx4+TCrbkm/ieD8Qmz7NUA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "SocialMediaInfos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "SocialMediaInfos");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "94d18a1e-c5a7-4bdf-8df8-1ed2174bb0af", "AQAAAAIAAYagAAAAEGbfMQ1BxwbllIfVLCVtut3LRv9flFxTA4k+SpeS8WI4ILhWm0YgeVbIYQou7WP1SA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0a043ac7-8111-4b2a-b24e-d8f7c43ec913", "AQAAAAIAAYagAAAAENvPRXX7uIdZKRnH7bywU37BmkfR92LeJZZMQvpqcoJnxHro+fqlKgPIefdxknGLgg==" });
        }
    }
}
