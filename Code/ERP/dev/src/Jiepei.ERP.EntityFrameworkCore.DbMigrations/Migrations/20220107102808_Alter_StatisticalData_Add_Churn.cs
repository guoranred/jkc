using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class Alter_StatisticalData_Add_Churn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChurnCustomers",
                table: "StatisticalData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChurnRate",
                table: "StatisticalData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChurnCustomers",
                table: "StatisticalData");

            migrationBuilder.DropColumn(
                name: "ChurnRate",
                table: "StatisticalData");
        }
    }
}
