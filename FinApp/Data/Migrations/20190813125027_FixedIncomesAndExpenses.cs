using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FixedIncomesAndExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenceCategoryId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "ExpenceCategoryId",
                table: "Expenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenceCategoryId",
                table: "Incomes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpenceCategoryId",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
