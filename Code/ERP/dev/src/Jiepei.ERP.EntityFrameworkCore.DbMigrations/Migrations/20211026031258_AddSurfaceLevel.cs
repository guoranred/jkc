using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class AddSurfaceLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurfaceLevel",
                table: "SubOrderCncItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "表面处理等级");

            migrationBuilder.AddColumn<string>(
                name: "SurfaceLevelName",
                table: "SubOrderCncItem",
                type: "varchar(80)",
                nullable: true,
                comment: "表面处理等级名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurfaceLevel",
                table: "SubOrderCncItem");

            migrationBuilder.DropColumn(
                name: "SurfaceLevelName",
                table: "SubOrderCncItem");
        }
    }
}
