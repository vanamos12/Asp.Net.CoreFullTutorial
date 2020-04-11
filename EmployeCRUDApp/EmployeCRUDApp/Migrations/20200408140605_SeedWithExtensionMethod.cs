using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeCRUDApp.Migrations
{
    public partial class SeedWithExtensionMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Departement", "Email", "Name" },
                values: new object[] { 3, 3, "vandam@pragimtech.com", "vandam" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
