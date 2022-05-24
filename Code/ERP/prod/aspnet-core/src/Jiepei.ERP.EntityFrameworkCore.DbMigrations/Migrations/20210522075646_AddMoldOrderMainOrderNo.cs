using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class AddMoldOrderMainOrderNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: 0,
                comment: "订单状态(枚举)",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "订单状态(枚举)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderType",
                table: "Order",
                type: "int",
                nullable: true,
                defaultValue: 0,
                comment: "订单类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "订单类型");

            migrationBuilder.AddColumn<string>(
                name: "MainOrderNo",
                table: "Mold_Order",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "主订单编号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainOrderNo",
                table: "Mold_Order");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "订单状态(枚举)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0,
                oldComment: "订单状态(枚举)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderType",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "订单类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0,
                oldComment: "订单类型");
        }
    }
}
