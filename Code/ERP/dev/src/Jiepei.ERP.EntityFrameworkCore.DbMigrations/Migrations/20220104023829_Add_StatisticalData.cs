using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class Add_StatisticalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Inquiry = table.Column<int>(type: "int", nullable: false),
                    Quotable = table.Column<int>(type: "int", nullable: false),
                    Quoted = table.Column<int>(type: "int", nullable: false),
                    NotQuoted = table.Column<int>(type: "int", nullable: false),
                    ValidInquiryRate = table.Column<int>(type: "int", nullable: false),
                    FollowUps = table.Column<int>(type: "int", nullable: false),
                    PaymentOrders = table.Column<int>(type: "int", nullable: false),
                    PaymentSuccessRate = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    TotalProductionNumber = table.Column<int>(type: "int", nullable: false),
                    OrdersInProduction = table.Column<int>(type: "int", nullable: false),
                    ProductionRate = table.Column<int>(type: "int", nullable: false),
                    CompletedOrders = table.Column<int>(type: "int", nullable: false),
                    OrderCompletionRate = table.Column<int>(type: "int", nullable: false),
                    TotalParts = table.Column<int>(type: "int", nullable: false),
                    FinishedParts = table.Column<int>(type: "int", nullable: false),
                    PartCompletionRate = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticalData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticalData");
        }
    }
}
