using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class Alter_StatisticalData_Coulumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FollowUps",
                table: "StatisticalData",
                newName: "FollowUpsRate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FollowUpsRate",
                table: "StatisticalData",
                newName: "FollowUps");
        }
    }
}
