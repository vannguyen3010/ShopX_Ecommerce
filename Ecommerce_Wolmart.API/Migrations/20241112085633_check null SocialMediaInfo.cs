using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class checknullSocialMediaInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "596fffb7-230a-4bcc-bd16-168e6ac3ed3f", "AQAAAAIAAYagAAAAEN0CyNrPbNJnnZvpvyNZjkQfKWmtw0hXrbSgKedvhUaqZ5TjXDUwZRlqesAFjN1Ccg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e0409e8b-dc21-4232-9815-d2884ec53871", "AQAAAAIAAYagAAAAEO8ThlltYPpCdxEOVrwRq539TX1ixj+SBmsLXpdLdAe+m59JoRIjWl+VDC+JdytqVg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
