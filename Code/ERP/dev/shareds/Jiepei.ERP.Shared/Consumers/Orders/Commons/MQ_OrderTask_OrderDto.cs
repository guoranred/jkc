using Jiepei.ERP.Orders;
using System;

namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 通用订单同步信息
    /// </summary>
    public class MQ_OrderTask_OrderDto
    {
        /// <summary>
        /// 关联外部订单号
        /// </summary>
        public string ChannelOrderNo { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        public EnumApplicationArea ApplicationArea { get; set; }

        /// <summary>
        /// 预计年使用量
        /// </summary>
        public EnumUsage Usage { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 渠道来源
        /// </summary>
        public Guid Channel { get; set; }

        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellingMoney { get; set; }

        /// <summary>
        /// 代付金额
        /// </summary>
        public decimal PendingMoney { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidMoney { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EnumOrderStatus Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public bool? IsPay { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }
        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
    }
}
