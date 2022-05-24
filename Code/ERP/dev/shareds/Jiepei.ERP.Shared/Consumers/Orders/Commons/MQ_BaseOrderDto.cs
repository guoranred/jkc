using Jiepei.ERP.Orders;
using System;

namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 基础定义
    /// </summary>
    public class MQ_BaseOrderDto
    {
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; } = 10;

        /// <summary>
        /// 外部关联订单
        /// </summary>
        public string ChannelOrderNo { get; set; }
        /// <summary>
        /// 渠道用户Id
        /// </summary>
        public string ChannelUserId { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public Guid Channel { get; set; }
    }
}
