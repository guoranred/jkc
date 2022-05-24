using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Pays;
using Jiepei.ERP.Orders.SubOrders;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.Orders.EntityFrameworkCore
{
    public static class OrdersDbContextModelCreatingExtensions
    {
        public static void ConfigureOrders(
            this ModelBuilder builder,
            Action<OrdersModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OrdersModelBuilderConfigurationOptions(
                OrdersDbProperties.DbTablePrefix,
                OrdersDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);


            #region Orders
            builder.Entity<Order>(b =>
            {
                b.ToTable("Order");

                b.HasComment("订单表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.OrderNo).HasComment("订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.ChannelUserId).HasComment("用户编号").HasColumnType("varchar(36)");
                b.Property(x => x.ChannelOrderNo).HasComment("关联外部订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.CustomerRemark).HasComment("备注").HasMaxLength(256);
                b.Property(x => x.ChannelId).HasComment("渠道");
                b.Property(x => x.Cost).HasComment("总成本").HasColumnType("decimal(18,2)");
                b.Property(x => x.SellingPrice).HasComment("销售价").HasColumnType("decimal(18,2)");
                b.Property(x => x.PendingMoney).HasComment("代付金额").HasColumnType("decimal(18,2)");
                b.Property(x => x.PaidMoney).HasComment("已付金额").HasColumnType("decimal(18,2)");
                b.Property(x => x.OrderType).HasComment("订单类型");
                b.Property(x => x.Status).HasComment("订单状态(枚举)");
                b.Property(x => x.IsPay).HasComment("是否付款").HasDefaultValue(false);
                b.Property(x => x.PayTime).HasComment("付款时间");
                b.Property(x => x.PayMode).HasComment("付款模式");
                b.Property(x => x.CustomerServiceRemark).HasComment("客服备注").HasMaxLength(256);
                b.Property(x => x.DeliveryDays).HasComment("交期天数");
                b.Property(x => x.DeliveryDate).HasComment("交期");
                b.Property(x => x.OrganizationUnitId).HasComment("组织单元Id");
                b.Property(x => x.CustomerService).HasComment("客服人员");
                b.Property(x => x.Engineer).HasComment("工程师");
                b.Property(x => x.TrackingNo).HasComment("快递单号").HasColumnType("varchar(32)");
                b.Property(x => x.CourierCompany).HasComment("快递公司").HasMaxLength(32);
                #endregion

                #region 索引
                b.HasIndex(x => x.OrderNo, "Index_Order_OrderNo");
                b.HasIndex(x => x.ChannelUserId, "Index_Order_ChannelUserId");
                b.HasIndex(x => x.ChannelOrderNo, "Index_Order_ChannelOrderNo");
                //b.HasIndex(x =>x.OrganizationUnitId, "Index_Order_OrganizationUnitId");
                #endregion
            });

            builder.Entity<OrderLog>(b =>
            {
                b.ToTable("OrderLog");
                b.HasComment("订单操作日志表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.OrderNo).HasComment("订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.Content).HasMaxLength(256);
                #endregion

                #region 索引
                b.HasIndex(x => x.OrderNo, "Index_OrderLog_OrderNo");
                #endregion
            });

            builder.Entity<OrderDelivery>(b =>
            {
                b.ToTable("OrderDelivery");
                b.HasComment("订单收货信息表");
                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.OrderNo).HasComment("订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.Weight).HasComment("总重量").HasDefaultValue(0.00m).HasColumnType("decimal(18,2)");
                b.Property(x => x.ReceiverName).HasComment("收货人").HasMaxLength(50);
                b.Property(x => x.ReceiverCompany).HasComment("收货公司名").HasMaxLength(128);
                b.Property(o => o.ProvinceCode).IsRequired().HasMaxLength(30).HasComment("省code");
                b.Property(o => o.ProvinceName).IsRequired().HasMaxLength(30).HasComment("省Name");
                b.Property(o => o.CityCode).IsRequired().HasMaxLength(30).HasComment("市code");
                b.Property(o => o.CityName).IsRequired().HasMaxLength(30).HasComment("市Name");
                b.Property(o => o.CountyCode).IsRequired().HasMaxLength(30).HasComment("县区code");
                b.Property(o => o.CountyName).IsRequired().HasMaxLength(30).HasComment("县区Name");
                b.Property(x => x.ReceiverAddress).HasComment("收货详细地址").HasMaxLength(256);
                b.Property(x => x.ReceiverTel).HasComment("收货人联系方式").HasMaxLength(50);
                b.Property(x => x.OrderContactName).HasComment("订单联系人").HasMaxLength(20);
                b.Property(x => x.OrderContactMobile).HasComment("订单联系人手机号").HasColumnType("varchar(32)");
                b.Property(x => x.OrderContactQQ).HasComment("订单联系人QQ").HasColumnType("varchar(16)");
                #endregion

                #region 索引
                b.HasIndex(x => x.OrderNo, "Index_Delivery_OrderNO");
                #endregion
            });

            builder.Entity<OrderCost>(b =>
            {
                b.ToTable("OrderCost");
                b.HasComment("订单费用信息表");
                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.OrderNo).HasComment("订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.ProMoney).HasComment("产品费").HasColumnType("decimal(18,2)");
                b.Property(x => x.ShipMoney).HasComment("运费").HasColumnType("decimal(18,2)");
                b.Property(x => x.TaxMoney).HasComment("税费").HasColumnType("decimal(18,2)");
                b.Property(x => x.TaxPoint).HasComment("税点").HasColumnType("decimal(18,2)");
                b.Property(x => x.DiscountMoney).HasComment("优惠金额").HasColumnType("decimal(18,2)");
                #endregion

                #region 索引
                b.HasIndex(x => x.OrderNo, "Index_Cost_OrderNo");
                #endregion
            });

            builder.Entity<OrderExtra>(b =>
            {
                b.ToTable("OrderExtra");
                b.HasComment("订单扩展表");
                b.ConfigureByConvention();
                b.Property(x => x.OrderId).HasComment("主订单Id");
            });

            #endregion

            #region SubOrders
            builder.Entity<SubOrder>(b =>
            {
                b.ToTable("SubOrder");
                b.HasComment("子订单表");
                b.ConfigureByConvention();

                b.Property(x => x.OrderId).HasComment("主订单Id");
                b.Property(x => x.OrderNo).HasComment("订单号").HasColumnType("varchar(32)");
                b.Property(x => x.ChannelUserId).HasComment("用户编号").HasColumnType("varchar(36)");
                b.Property(x => x.ChannelOrderNo).HasComment("关联外部订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.ChannelId).HasComment("渠道");
                b.Property(x => x.Cost).HasComment("总成本").HasColumnType("decimal(18,2)");
                b.Property(x => x.SellingPrice).HasComment("销售价").HasColumnType("decimal(18,2)");
                b.Property(x => x.OrderType).HasComment("订单类型");
                b.Property(x => x.OrganizationUnitId).HasComment("组织单元Id");
                b.Property(x => x.Status).HasComment("订单状态");
                b.Property(x => x.Remark).HasComment("备注").HasMaxLength(256);

                #region 索引
                b.HasIndex(b => b.OrderId, "Index_SubOrder_OrderId");
                b.HasIndex(b => b.OrderNo, "Index_SubOrder_OrderNo");
                //b.HasIndex(b =>b.OrganizationUnitId, "Index_SubOrder_OrganizationUnitId");
                #endregion
            });

            builder.Entity<SubOrderFlow>(b =>
            {
                b.ToTable("SubOrderFlow");
                b.HasComment("子订单流程表");
                b.ConfigureByConvention();

                b.Property(x => x.OrderNo).HasComment("订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.Content).HasMaxLength(256);
                b.Property(x => x.Type).HasComment("流程类型：审批、报价等");
                b.Property(x => x.Remark).HasComment("备注").HasMaxLength(256);

                b.HasIndex(b => b.OrderNo, "Index_SubOrderFlow_OrderNo");
            });
            #endregion

            #region SubOrderMoldItems
            builder.Entity<SubOrderMoldItem>(b =>
            {
                b.ToTable("SubOrderMoldItem");
                b.HasComment("模具订单表");
                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.SubOrderId).HasComment("订单编号").HasColumnType("varchar(36)");
                b.Property(x => x.ProductName).HasMaxLength(50).HasComment("产品名称");
                b.Property(x => x.Picture).HasComment("产品图片").HasColumnType("varchar(1000)");
                b.Property(x => x.FileName).HasMaxLength(100).HasComment("产品文件名称");
                b.Property(x => x.FilePath).HasComment("产品文件路径").HasColumnType("varchar(300)");
                b.Property(x => x.Material).HasComment("材料");
                b.Property(x => x.Surface).HasComment("表面处理");
                b.Property(x => x.Size).HasComment("产品尺寸");
                b.Property(x => x.Color).HasComment("颜色");
                b.Property(x => x.Quantity).HasDefaultValue(0).HasComment("数量");
                b.Property(x => x.ApplicationArea).HasComment("应用领域");
                b.Property(x => x.Usage).HasComment("预计年使用量 ");
                #endregion

                #region 索引
                b.HasIndex(x => x.SubOrderId, "Index_SubOrderMoldItem_SubOrderId");
                #endregion
            });
            #endregion

            #region SubOrderInjectionItems
            builder.Entity<SubOrderInjectionItem>(b =>
            {
                b.ToTable("SubOrderInjectionItem");
                b.HasComment("注塑订单表");
                b.ConfigureByConvention();

                #region 注释
                b.Property(x => x.SubOrderId).IsRequired().HasMaxLength(30).HasComment("订单编号");
                b.Property(x => x.ProductName).HasMaxLength(50).HasComment("产品名称");
                b.Property(x => x.Picture).HasComment("产品图片").HasColumnType("varchar(1000)");
                b.Property(x => x.FileName).HasMaxLength(100).HasComment("产品文件名称");
                b.Property(x => x.FilePath).HasComment("产品文件路径").HasColumnType("varchar(300)");
                b.Property(x => x.Material).HasComment("材料");
                b.Property(x => x.Surface).HasComment("表面处理");
                b.Property(x => x.Size).HasComment("产品尺寸");
                b.Property(x => x.Color).HasComment("颜色");
                b.Property(x => x.Quantity).HasDefaultValue(0).HasComment("数量");
                b.Property(x => x.PackMethod).HasComment("包装方式");
                #endregion

                #region 索引
                b.HasIndex(x => x.SubOrderId, "Index_SubOrderInjectionItem_SubOrderId");
                #endregion

            });

            #endregion

            #region SubOrderCncItems
            builder.Entity<SubOrderCncItem>(b =>
            {
                b.ToTable("SubOrderCncItem");
                b.HasComment("Cnc订单表");
                b.ConfigureByConvention();

                #region 注释
                b.Property(x => x.SubOrderId).IsRequired().HasMaxLength(30).HasComment("订单编号");
                b.Property(x => x.ProductName).HasMaxLength(50).HasComment("产品名称");
                b.Property(x => x.Picture).HasComment("产品图片").HasColumnType("varchar(1000)");
                b.Property(x => x.FileName).HasMaxLength(100).HasComment("产品文件名称");
                b.Property(x => x.FilePath).HasComment("产品文件路径").HasColumnType("varchar(300)");
                b.Property(x => x.Material).HasComment("材料");
                b.Property(x => x.MaterialName).HasComment("材料名称").HasColumnType("varchar(80)"); ;
                b.Property(x => x.Surface).HasComment("表面处理");
                b.Property(x => x.SurfaceName).HasComment("表面处理名称").HasColumnType("varchar(80)");
                b.Property(x => x.Size).HasComment("产品尺寸");
                b.Property(x => x.Quantity).HasDefaultValue(0).HasComment("数量");
                b.Property(x => x.ApplicationArea).HasComment("应用领域");
                b.Property(x => x.SurfaceLevel).HasComment("表面处理等级");
                b.Property(x => x.SurfaceLevelName).HasComment("表面处理等级名称").HasColumnType("varchar(80)");
                #endregion

                #region 索引
                b.HasIndex(x => x.SubOrderId, "Index_SubOrderCncItem_SubOrderId");
                #endregion

            });

            #endregion

            #region SubOrderThreeDItem
            builder.Entity<SubOrderThreeDItem>(b =>
            {
                b.ToTable("SubOrderThreeDItem");
                b.HasComment("3d订单项目表");
                b.ConfigureByConvention();

                #region 注释
                b.Property(x => x.SubOrderId).IsRequired().HasMaxLength(30).HasComment("订单编号");
                b.Property(x => x.FileName).HasComment("产品文件名称").HasColumnType("varchar(100)");
                b.Property(x => x.FilePath).HasComment("产品文件路径").HasColumnType("varchar(200)");
                b.Property(x => x.Thumbnail).HasComment("缩略图").HasColumnType("varchar(1000)");
                b.Property(x => x.Color).HasComment("可选类型 -- 颜色").HasColumnType("varchar(300)");
                b.Property(x => x.MaterialName).HasComment("材料名称").HasColumnType("varchar(50)");
                b.Property(x => x.MaterialId).HasComment("材料id").HasColumnType("varchar(50)");
                b.Property(x => x.Count).HasDefaultValue(0).HasComment("数量");
                b.Property(x => x.Volume).HasComment("文件体积(立方毫米)").HasColumnType("decimal(18,2)");
                b.Property(x => x.Size).HasComment("产品尺寸").HasColumnType("varchar(50)");
                b.Property(x => x.SupportVolume).HasComment("支撑体积(立方毫米) 【需要软件计算，目前未使用到】 ").HasColumnType("decimal(18,2)");
                b.Property(x => x.HandleMethod).HasComment("后处理方式(表面处理)  ").HasColumnType("varchar(20)");
                b.Property(x => x.HandleMethodDesc).HasComment("后处理描述 ").HasColumnType("varchar(1000)");
                b.Property(x => x.HandleFee).HasComment("后处理费用 ").HasColumnType("decimal(18,2)");
                b.Property(x => x.Price).HasComment("单价").HasColumnType("decimal(18,2)");
                b.Property(x => x.OrginalMoney).HasComment("总价").HasColumnType("decimal(18,2)");
                b.Property(x => x.DeliveryDays).HasComment("交期天数").HasColumnType("int");
                b.Property(x => x.SupplierFileId).HasComment("打印文件Id ").HasColumnType("varchar(100)");
                b.Property(x => x.SupplierPreViewId).HasComment("预览文件ID ").HasColumnType("varchar(100)");
                b.Property(x => x.SupplierOrderCode).HasComment("供应商订单编号 ").HasColumnType("varchar(36)");
                b.Property(x => x.FileMD5).HasComment("文件MD5").HasColumnType("varchar(36)");
                #endregion

                #region 索引
                b.HasIndex(x => x.SubOrderId, "Index_SubOrderThreeDItem_SubOrderId");
                #endregion

            });
            #endregion

            #region SubOrderSheetMetalItem
            builder.Entity<SubOrderSheetMetalItem>(b =>
            {
                b.ToTable("SubOrderSheetMetalItem");
                b.HasComment("钣金订单项目表");
                b.ConfigureByConvention();

                #region 注释
                b.Property(x => x.SubOrderId).IsRequired().HasMaxLength(30).HasComment("订单编号");
                b.Property(x => x.FileName).HasComment("产品文件名称").HasColumnType("varchar(100)");
                b.Property(x => x.FilePath).HasComment("产品文件路径").HasColumnType("varchar(200)");
                b.Property(x => x.ProductNum).HasComment("产品套数").HasColumnType("int");
                b.Property(x => x.AssembleType).HasComment("是否成套组装 1是0否").HasColumnType("bit");
                b.Property(x => x.NeedDesign).HasComment("是否需要设计 1是 0否").HasColumnType("bit");
                b.Property(x => x.ProcessParameters).HasComment("工艺参数字符串").HasColumnType("varchar(max)");
                b.Property(x => x.PreviewUrl).HasComment("3d文件预览地址").HasColumnType("varchar(500)");
                b.Property(x => x.PurchasedParts).HasComment("配件采购 0 不需要 1 自供 2 代采").HasColumnType("int");
                b.Property(x => x.Thumbnail).HasComment("缩略图").HasColumnType("varchar(1000)");
                b.Property(x => x.SupplierFileId).HasComment("打印文件Id ").HasColumnType("varchar(100)");
                b.Property(x => x.SupplierPreViewId).HasComment("预览文件ID ").HasColumnType("varchar(100)");
                b.Property(x => x.FileMD5).HasComment("文件MD5").HasColumnType("varchar(36)");
                b.Property(x => x.ProductRemark).HasComment("产品备注").HasMaxLength(256);
                #endregion

                #region 索引
                b.HasIndex(x => x.SubOrderId, "Index_SubOrderSheetMetalItem_SubOrderId");
                #endregion

            });
            #endregion


            #region Channels
            builder.Entity<Channel>(b =>
            {
                b.ToTable("Channel");

                b.HasComment("渠道表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.ChannelName).HasComment("渠道名称").HasColumnType("varchar(64)");
                b.Property(x => x.IsEnable).HasComment("是否启用").HasColumnType("bit");
                #endregion
            });
            #endregion

            #region D3Material
            builder.Entity<D3Material>(b =>
            {
                b.ToTable("D3Material");

                b.HasComment("3d材料表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.Code).HasComment("编号").HasColumnType("varchar(50)");
                b.Property(x => x.PartCode).HasComment("型号").HasColumnType("varchar(50)");
                b.Property(x => x.Name).HasComment("名称").HasColumnType("varchar(50)");
                b.Property(x => x.Category).HasComment("分类").HasColumnType("varchar(50)");
                b.Property(x => x.Density).HasComment("密度").HasColumnType("varchar(50)");
                b.Property(x => x.Delivery).HasComment("交期(天)").HasColumnType("int");
                b.Property(x => x.Attr).HasComment("特性").HasColumnType("varchar(1000)");
                b.Property(x => x.Excellence).HasComment("优点").HasColumnType("varchar(1000)");
                b.Property(x => x.Short).HasComment("缺点").HasColumnType("varchar(1000)");
                b.Property(x => x.Color).HasComment("颜色(对应内贸可选类型) 是否枚举？").HasColumnType("varchar(50)");
                b.Property(x => x.MinSinWeight).HasComment("最小单件重量").HasColumnType("decimal(18,2)");
                b.Property(x => x.Note).HasComment("备注").HasColumnType("varchar(1000)");
                #endregion
            });
            #endregion

            #region MaterialPrice
            builder.Entity<MaterialPrice>(b =>
            {
                b.ToTable("MaterialPrice");

                b.HasComment("材料渠道价格表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.MaterialId).HasComment("材料id");
                b.Property(x => x.OrderType).HasComment("适用订单类型").HasColumnType("int");
                b.Property(x => x.ChannelId).HasComment("渠道id");
                b.Property(x => x.Price).HasComment("单价").HasColumnType("decimal(18,2)");
                b.Property(x => x.StartPrice).HasComment("起步价").HasColumnType("decimal(18,2)");
                b.Property(x => x.Discount).HasComment("折扣比率").HasColumnType("decimal(18,2)");
                b.Property(x => x.IsSale).HasComment("是否上架").HasColumnType("bit");
                b.Property(x => x.SeqNo).HasComment("前台排序").HasColumnType("int");
                b.Property(x => x.Note).HasComment("备注").HasColumnType("varchar(1000)");
                b.Property(x => x.UnitStartPrice).IsRequired().HasComment("单件起步价").HasColumnType("decimal(18,2)");
                #endregion
            });
            #endregion

            #region MaterialSupplier
            builder.Entity<MaterialSupplier>(b =>
            {
                b.ToTable("MaterialSupplier");

                b.HasComment("材料物流表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.MaterialId).HasComment("材料id");
                b.Property(x => x.SupplierId).HasComment("供应商Id");
                b.Property(x => x.SupplierName).HasComment("供应商名称");
                b.Property(x => x.SupplierSpuId).HasComment("供应商SPU").HasColumnType("varchar(50)");
                #endregion
            });
            #endregion

            #region Pays
            builder.Entity<OrderPayLog>(b =>
            {
                b.ToTable("OrderPayLog");

                b.HasComment("订单支付日志表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.MemberId).HasComment("客户Id");
                b.Property(x => x.MemberName).HasComment("客户姓名").HasColumnType("varchar(50)");
                b.Property(x => x.PayCode).IsRequired().HasComment("支付单号").HasColumnType("varchar(100)");
                b.Property(x => x.PayType).HasComment("支付方式");
                b.Property(x => x.TotalAmout).HasComment("支付金额(总金额-优惠金额)").HasColumnType("decimal(18,2)");
                b.Property(x => x.IsPaySuccess).HasComment("是否支付成功").HasDefaultValue(false);
                b.Property(x => x.Remark).HasComment("备注").HasColumnType("varchar(500)");
                #endregion

                #region 索引
                b.HasIndex(x => x.PayCode, "Index_OrderPayLog_PayCode");
                #endregion
            });

            builder.Entity<OrderPayDetailLog>(b =>
            {
                b.ToTable("OrderPayDetailLog");

                b.HasComment("订单支付详细日志表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.PayLogId).IsRequired();
                b.Property(x => x.OrderNo).HasComment("订单编号").HasColumnType("varchar(32)");
                b.Property(x => x.SellingMoney).HasComment("销售金额").HasColumnType("decimal(18,2)");
                b.Property(x => x.DiscountAmount).HasComment("优惠金额").HasColumnType("decimal(18,2)");
                b.Property(x => x.FlowType).HasComment("交易流水类型");
                b.Property(x => x.IsSuccess).HasComment("是否成功").HasDefaultValue(false);
                #endregion

                #region 索引
                b.HasIndex(x => x.PayLogId, "Index_OrderPayDetailLog_PayLogId");
                b.HasIndex(x => x.OrderNo, "Index_OrderPayDetailLog_OrderNo");
                #endregion
            });
            #endregion
        }
    }
}