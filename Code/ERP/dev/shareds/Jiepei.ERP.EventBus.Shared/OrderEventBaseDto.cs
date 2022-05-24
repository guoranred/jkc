using Jiepei.ERP.Orders;
using System;

namespace Jiepei.ERP.EventBus.Shared
{
    /// <summary>
    /// 订单基类 事件总线ETO
    /// </summary>
    [Serializable]
    public class OrderEventBaseDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }
    }
}
