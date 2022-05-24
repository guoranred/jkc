namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 通用订单同步地址信息
    /// </summary>
    public class MQ_OrderTask_OrderDeliveryDto
    {
        /// <summary>
        /// 总重量
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货公司名
        /// </summary>
        public string ReceiverCompany { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { get; set; }

        /// <summary>
        /// 订单联系人
        /// </summary>
        public string OrderContactName { get; set; }

        /// <summary>
        /// 订单联系人手机号
        /// </summary>
        public string OrderContactMobile { get; set; }

        /// <summary>
        /// 订单联系人QQ
        /// </summary>
        public string OrderContactQQ { get; set; }
    }
}
