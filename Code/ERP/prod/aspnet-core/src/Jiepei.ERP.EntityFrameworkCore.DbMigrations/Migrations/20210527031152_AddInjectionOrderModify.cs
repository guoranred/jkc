using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class AddInjectionOrderModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "Mold_Order",
                type: "int",
                nullable: true,
                defaultValue: 0,
                comment: "数量",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "数量");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "InjectionOrder",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "尺寸",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "产品文件名称");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "InjectionOrder",
                type: "varchar(200)",
                nullable: true,
                comment: "产品图片",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "产品图片");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "InjectionOrder",
                type: "varchar(32)",
                nullable: false,
                comment: "订单号",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "订单号");

            migrationBuilder.AlterColumn<string>(
                name: "MoldOrderNo",
                table: "InjectionOrder",
                type: "varchar(32)",
                nullable: false,
                comment: "模具关联订单号",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "模具关联订单号");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "InjectionOrder",
                type: "varchar(200)",
                nullable: true,
                comment: "产品文件路径",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "产品文件路径");

            migrationBuilder.AddColumn<string>(
                name: "MainOrderNo",
                table: "InjectionOrder",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "",
                comment: "主订单号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainOrderNo",
                table: "InjectionOrder");

            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "Mold_Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "数量",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0,
                oldComment: "数量");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "InjectionOrder",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "产品文件名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "尺寸");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "InjectionOrder",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "产品图片",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "产品图片");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "InjectionOrder",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "订单号",
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldComment: "订单号");

            migrationBuilder.AlterColumn<string>(
                name: "MoldOrderNo",
                table: "InjectionOrder",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "模具关联订单号",
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldComment: "模具关联订单号");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "InjectionOrder",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "产品文件路径",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "产品文件路径");
        }
    }
}
