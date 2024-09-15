using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class converboolOrderstatusOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OrderStatus",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ddff62c7-9afd-4f8c-898c-4be21c55a796", "AQAAAAIAAYagAAAAEC9eGezh7qVlOVioR8xNJgcONhmMwPFOLiGmlw5HxuplJz4QnOf9nd2mfZakBvm8bg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "96f25b80-4d89-4cf2-ad28-c21c26f0c356", "AQAAAAIAAYagAAAAEDmwHByMWKIoNtmWG/i3oE+WXKSm2ZZbiCsNhZMcp+0s48N0UJbZfXIGanP53RxE2g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65d05f41-10d2-4629-850e-efb599bd614c", "AQAAAAIAAYagAAAAEBbqZKQh0L/SYJbUt4hWW+Y71G1Cbqui3NHKUmqhJ8/rvHeF5qQcKmk6etITdVQ7Qw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9d6d3d7b-a4f2-4a65-8b8c-90aa89c2654c", "AQAAAAIAAYagAAAAEKH1Hak+TCEWl4WYG+RDfyUkoRuzUR/SjyKB6xbeMN8vu/YsCfZ1+uFWWDPg3MzHbQ==" });
        }
    }
}
