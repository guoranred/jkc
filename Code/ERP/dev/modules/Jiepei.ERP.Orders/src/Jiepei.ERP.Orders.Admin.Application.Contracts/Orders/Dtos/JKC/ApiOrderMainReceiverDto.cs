
namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiOrderMainReceiverDto
    {
        /// <summary>
        /// 订单包编号
        /// </summary>
        public string GroupNo { set; get; }

        /// <summary>
        /// 收货省名
        /// </summary>
        public string ReceiverState { set; get; }

        /// <summary>
        /// 收货城市名
        /// </summary>
        public string ReceiverCity { set; get; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverName { set; get; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string ReceiverTel { set; get; }

        /// <summary>
        /// 订单联系人
        /// </summary>
        public string OrderContactName { set; get; }

        /// <summary>
        /// 订单联系人手机
        /// </summary>
        public string OrderContactMobile { set; get; }

        /// <summary>
        /// 收货邮编
        /// </summary>
        public string ReceiverPostCode { set; get; }

        /// <summary>
        /// 收货公司名
        /// </summary>
        public string ReceiverCompany { set; get; }

        /// <summary>
        /// 收货省Id
        /// </summary>
        public int? ReceiverStateId { set; get; }

        /// <summary>
        /// 收货地址城市Id
        /// </summary>
        public int? ReceiverCityId { set; get; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReceiverAddress { set; get; }

        /// <summary>
        /// 发货方式
        /// </summary>
        public string ShipType { set; get; }
    }
}
