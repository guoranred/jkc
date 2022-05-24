using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class Update_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "SubOrderSheetMetalItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurfaceProcess",
                table: "SubOrderSheetMetalItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelUserId",
                table: "Order",
                type: "varchar(36)",
                nullable: true,
                comment: "用户编号",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldComment: "用户编号");

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "SubOrderSheetMetalItem");

            migrationBuilder.DropColumn(
                name: "SurfaceProcess",
                table: "SubOrderSheetMetalItem");

            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "ChannelUserId",
                table: "Order",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "",
                comment: "用户编号",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true,
                oldComment: "用户编号");
        }
    }
}
