using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiepei.ERP.Migrations
{
    public partial class ModuleInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExterUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "账号"),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "密码"),
                    Mobile = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "联系方式"),
                    Origin = table.Column<int>(type: "int", nullable: true, defaultValue: 0, comment: "渠道来源"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                },
                comment: "客户表");

            migrationBuilder.CreateTable(
                name: "Customer_Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "省"),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "城市"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "详细地址"),
                    Contacts = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "联系人"),
                    Mobile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "联系电话"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否默认地址"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Address", x => x.Id);
                },
                comment: "客户地址表");

            migrationBuilder.CreateTable(
                name: "EasyAbpDataDictionaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DisplayText = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpDataDictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InjectionOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单号"),
                    MoldOrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "模具关联订单号"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "订单状态"),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "特殊备注"),
                    PackMethod = table.Column<int>(type: "int", nullable: true, comment: "包装方式"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "产品名称"),
                    Picture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "产品图片"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "产品文件名称"),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "产品文件路径"),
                    Material = table.Column<int>(type: "int", nullable: true, comment: "产品材质(材料)"),
                    Qty = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    Size = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "产品文件名称"),
                    Surface = table.Column<int>(type: "int", nullable: true, comment: "表面处理"),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "颜色"),
                    CustomerRemark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "客服备注"),
                    DeliveryDays = table.Column<int>(type: "int", nullable: true, comment: "交期天数"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "交期"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InjectionOrder", x => x.Id);
                },
                comment: "注塑订单");

            migrationBuilder.CreateTable(
                name: "Mold_Flow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单编号"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "流程类型"),
                    Remark = table.Column<string>(type: "varchar(500)", nullable: true, comment: "备注"),
                    Note = table.Column<string>(type: "varchar(500)", nullable: true, comment: "操作记录"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mold_Flow", x => x.Id);
                },
                comment: "模具操作流程表");

            migrationBuilder.CreateTable(
                name: "Mold_Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单编号"),
                    ProName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "产品名称"),
                    Picture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "产品图片"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "文件名称"),
                    FilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "文件路径"),
                    Material = table.Column<int>(type: "int", nullable: true, comment: "材料"),
                    Surface = table.Column<int>(type: "int", nullable: true, comment: "表面处理"),
                    Long = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "长"),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "宽"),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "高"),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "重量"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "颜色"),
                    Qty = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "数量"),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValue: 0, comment: "模具订单状态"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mold_Order", x => x.Id);
                },
                comment: "模具订单表");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单编号"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "用户编号"),
                    ExterOrderNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "关联外部订单编号"),
                    ApplicationArea = table.Column<int>(type: "int", nullable: true, comment: "应用领域(枚举)"),
                    Usage = table.Column<int>(type: "int", nullable: true, comment: "预计年使用量"),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "备注"),
                    Origin = table.Column<int>(type: "int", nullable: true, comment: "渠道来源（枚举）"),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "总成本"),
                    SellingMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "销售价"),
                    PendingMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "代付金额"),
                    PaidMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "已付金额"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "订单状态(枚举)"),
                    OrderType = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "订单类型"),
                    IsPay = table.Column<bool>(type: "bit", nullable: true, defaultValue: false, comment: "是否付款"),
                    PayTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "付款时间"),
                    PayMode = table.Column<byte>(type: "tinyint", nullable: true, comment: "付款模式"),
                    Note = table.Column<string>(type: "nvarchar(256)", nullable: true, comment: "客服备注"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                },
                comment: "订单表");

            migrationBuilder.CreateTable(
                name: "Order_Cost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单编号"),
                    ProMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "产品费"),
                    ShipMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "运费"),
                    TaxMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "税费"),
                    TaxPoint = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "税点"),
                    DiscountMoney = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "优惠金额"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Cost", x => x.Id);
                },
                comment: "订单费用信息表");

            migrationBuilder.CreateTable(
                name: "Order_Delivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单编号"),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0.00m, comment: "总重量"),
                    ReceiverName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收货人"),
                    ReceiverCompany = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "收货公司名"),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "收货详细地址"),
                    ReceiverTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收货人联系方式"),
                    OrderContactName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "订单联系人"),
                    OrderContactMobile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "订单联系人手机号"),
                    OrderContactQQ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "订单联系人QQ"),
                    TrackingNo = table.Column<string>(type: "varchar(32)", nullable: true, comment: "快递单号"),
                    CourierCompany = table.Column<string>(type: "varchar(128)", nullable: true, comment: "快递公司"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Delivery", x => x.Id);
                },
                comment: "订单收货信息表");

            migrationBuilder.CreateTable(
                name: "Order_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "订单编号"),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Log", x => x.Id);
                },
                comment: "订单操作日志表");

            migrationBuilder.CreateTable(
                name: "EasyAbpDataDictionaryItems",
                columns: table => new
                {
                    DataDictionaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayText = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpDataDictionaryItems", x => new { x.Code, x.DataDictionaryId });
                    table.ForeignKey(
                        name: "FK_EasyAbpDataDictionaryItems_EasyAbpDataDictionaries_DataDictionaryId",
                        column: x => x.DataDictionaryId,
                        principalTable: "EasyAbpDataDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpDataDictionaries_Code",
                table: "EasyAbpDataDictionaries",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpDataDictionaryItems_Code_DataDictionaryId",
                table: "EasyAbpDataDictionaryItems",
                columns: new[] { "Code", "DataDictionaryId" });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpDataDictionaryItems_DataDictionaryId",
                table: "EasyAbpDataDictionaryItems",
                column: "DataDictionaryId");

            migrationBuilder.CreateIndex(
                name: "Index_MoldFlow_OrderNo",
                table: "Mold_Flow",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "Index_MoldOrder_OrderNO",
                table: "Mold_Order",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "Index_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "Index_Order_ExterOrderNo",
                table: "Order",
                column: "ExterOrderNo");

            migrationBuilder.CreateIndex(
                name: "Index_Order_OrderNo",
                table: "Order",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "Index_Cost_OrderNo",
                table: "Order_Cost",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "Index_Delivery_OrderNO",
                table: "Order_Delivery",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "Index_OrderLog_OrderNo",
                table: "Order_Log",
                column: "OrderNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Customer_Address");

            migrationBuilder.DropTable(
                name: "EasyAbpDataDictionaryItems");

            migrationBuilder.DropTable(
                name: "InjectionOrder");

            migrationBuilder.DropTable(
                name: "Mold_Flow");

            migrationBuilder.DropTable(
                name: "Mold_Order");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Order_Cost");

            migrationBuilder.DropTable(
                name: "Order_Delivery");

            migrationBuilder.DropTable(
                name: "Order_Log");

            migrationBuilder.DropTable(
                name: "EasyAbpDataDictionaries");
        }
    }
}
