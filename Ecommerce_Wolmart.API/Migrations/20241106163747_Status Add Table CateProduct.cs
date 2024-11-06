using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class StatusAddTableCateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CateProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ad9b673e-24ec-49ca-8c31-c7a4f71d0337", "AQAAAAIAAYagAAAAEApnEQeGQcxyiXq7ZPbbsIxT+YrW9Qv9rlClXCrdx6WpK+pKfM1mvq4UZEcb0AUpng==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ee80520-3eff-4b93-91ac-677938b8bd9e", "AQAAAAIAAYagAAAAED4gFrerIF+ONrbQe9MhUVLsJGqdbdLt94vRlBoc/ZC6Xj0ev0u5Kr4+C+fNUBLwKw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CateProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba43e3f5-7914-4edf-8b62-1479abf52b44", "AQAAAAIAAYagAAAAEDCPqHBg5c7+5k2JZAGPKTC+sX+IixNqbvJ1e994XtMLiioHHSfSseMXKN1e8rzj7g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9f559187-9a74-467d-bb57-95251c95e460", "AQAAAAIAAYagAAAAELzkAmHi88SZC1cWzmE39j7Xvq+FyE08iYC0Yc5sYI/Rhm98401pRUHkMscJsmSJAQ==" });
        }
    }
}
