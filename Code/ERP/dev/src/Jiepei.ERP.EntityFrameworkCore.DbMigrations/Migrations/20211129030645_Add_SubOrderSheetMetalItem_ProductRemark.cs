using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class Add_SubOrderSheetMetalItem_ProductRemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductRemark",
                table: "SubOrderSheetMetalItem",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                comment: "产品备注");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductRemark",
                table: "SubOrderSheetMetalItem");
        }
    }
}
