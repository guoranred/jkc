using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class Add_JobNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobNumber",
                table: "CustomerService",
                type: "varchar(10)",
                nullable: true,
                comment: "工号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobNumber",
                table: "CustomerService");
        }
    }
}
