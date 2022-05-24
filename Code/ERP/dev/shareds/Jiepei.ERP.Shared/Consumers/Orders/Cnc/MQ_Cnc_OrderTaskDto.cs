using Jiepei.ERP.Cncs;
using Jiepei.ERP.SubOrders;
using System.Collections.Generic;

namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 模具渠道订单同步定义
    /// </summary>
    public class MQ_Cnc_OrderTaskDto : MQ_BaseOrderDto
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
        public MQ_OrderTask_CncOrderDto CncOrder { get; set; }

        public List<MQ_OrderTask_CncBomDto> CncBoms { get; set; }
    }

    /// <summary>
    ///订单同步 模具信息
    /// </summary>
    public class MQ_OrderTask_CncOrderDto
    {
        /// <summary>
        /// 状态
        /// </summary>
        public EnumSubOrderStatus Status { get; set; }
    }

    public class MQ_OrderTask_CncBomDto
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
        /// 3D文件尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 材料
        /// </summary>
        public EnumCncMaterial? Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public EnumCncSurface? Surface { get; set; }

    }
}
