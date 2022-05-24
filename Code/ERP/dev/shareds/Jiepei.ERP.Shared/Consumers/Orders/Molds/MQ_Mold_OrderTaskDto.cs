
using Jiepei.ERP.Molds;
using Jiepei.ERP.SubOrders;

namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 模具渠道订单同步定义
    /// </summary>
    public class MQ_Mold_OrderTaskDto : MQ_BaseOrderDto
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
        /// 模具订单信息
        /// </summary>
        public MQ_OrderTask_MoldOrderDto MoldOrder { get; set; }
    }

    /// <summary>
    ///订单同步 模具信息
    /// </summary>
    public class MQ_OrderTask_MoldOrderDto
    {

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
        /// 材料
        /// </summary>
        public EnumMoldMaterial Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public EnumMoldSurface Surface { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public decimal? Long { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public decimal? Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

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
