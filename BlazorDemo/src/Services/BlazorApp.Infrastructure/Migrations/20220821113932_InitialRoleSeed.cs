using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Infrastructure.Migrations
{
    public partial class InitialRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34b6e780-6487-4f24-9cfa-512913ddd425", "583d63d4-0050-487c-a748-580577af887d", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb2e4f1b-3ffe-431b-b8c4-5a00390309a2", "483a1abc-2fbb-402f-b7b9-6c3c95600b0c", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34b6e780-6487-4f24-9cfa-512913ddd425");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb2e4f1b-3ffe-431b-b8c4-5a00390309a2");
        }
    }
}
