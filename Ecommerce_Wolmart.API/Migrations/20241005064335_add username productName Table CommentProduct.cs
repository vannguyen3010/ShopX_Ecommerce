using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class addusernameproductNameTableCommentProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CommentProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CommentProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ed84ec8c-9678-4c8d-8379-0d7423c01a84", "AQAAAAIAAYagAAAAEK3iDPPPKQI6+uLkEvlFH6FE3jZ4c8Ad2hQBK3L2HpBYJFBJLT4GEmMEXvlaBJXyHQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d93acdc2-3ec3-4b1e-8671-3468c6f352b9", "AQAAAAIAAYagAAAAELTBPnwbPYXD8iiu3C/XPsZ1Go55nSBBkolEod4IxFGiaEk6cKWLbb8vvY9fKVv0cw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CommentProducts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CommentProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d749029c-5628-4816-824a-65da0da160b1", "AQAAAAIAAYagAAAAEPWzNSyLGuxxZKtLyBh+xdWO5jootZIH4xl7yXCziF0F5wcOykbwtq0Z+1b2kcaLoQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "75f29dcf-0ef8-4a40-b418-4074faf6b84e", "AQAAAAIAAYagAAAAEOjZnxJtewuzqN51bk66t9skp7ABREWvP3WOHlgURjvkqnEARZRW/PTa0v5jFS2n9A==" });
        }
    }
}
