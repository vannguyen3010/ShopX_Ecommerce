using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnititesProductisHot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHot",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aeaf720f-f86c-4c2d-8644-988359b78532", "AQAAAAIAAYagAAAAEMSFH5T3l+9OVyXYUv+Mp7ytoOxYWjLVsi1I+y3fwYEBEUy3BGLhiSTKLygzDBLiyQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6120e793-32c7-4d0b-a7fd-0e8d7fc329e2", "AQAAAAIAAYagAAAAEHskNIU1QriLw37thf/CU2VnnGHy16p4iDJV8Cz1np4XVN2HR3A9AZCFkal2IR6wXg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHot",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dacf13f1-a59b-48b0-b169-3791fcff739a", "AQAAAAIAAYagAAAAEPCRDBt6dfhk6/GW5CS9iCjeGN3W9A38X9hQXjKTKDkHWdZwjS7BG2NroHnpb7LXXA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e891811c-3747-4d8f-bdfe-1441fa906008", "AQAAAAIAAYagAAAAEAq1wzZDVr+qn5wRMG/71QFWXeRYlab6cLn+z6L83zXloZoanO9v+Jd0wD+nIpeF7g==" });
        }
    }
}
