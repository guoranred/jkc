
using Jiepei.ERP.Injections;
using Jiepei.ERP.SubOrders;

namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 模具渠道订单同步定义
    /// </summary>
    public class MQ_Injection_OrderTaskDto : MQ_BaseOrderDto
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public MQ_OrderTask_OrderDto Order { get; set; }

        /// <summary>
        /// 订单费用信息
        /// </summary>
        public MQ_OrderTask_OrderCostDto OrderCost { get; set; }

        /// <summary>
        /// 订单收件信息
        /// </summary>
        public MQ_OrderTask_OrderDeliveryDto OrderDelivery { get; set; }

        /// <summary>
        /// 注塑订单信息
        /// </summary>
        public MQ_OrderTask_InjectionOrderDto InjectionOrder { get; set; }
    }

    /// <summary>
    ///订单同步 模具信息
    /// </summary>
    public class MQ_OrderTask_InjectionOrderDto
    {
        /// <summary>
        /// 内贸模具订单号
        /// </summary>
        /// <value></value>
        public string ExternalMoldOrderNo { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 包装方式
        /// </summary>
        public EnumPackMethod? PackMethod { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public EnumInjectionMaterial? Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public EnumInjectionSurface? Surface { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        /// <value></value>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EnumSubOrderStatus Status { get; set; }


    }
}
