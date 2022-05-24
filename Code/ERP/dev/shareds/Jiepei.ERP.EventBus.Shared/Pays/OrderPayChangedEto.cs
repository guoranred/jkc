using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Pays
{
    /// <summary>
    /// 支付成功
    /// </summary>
    [EventName("Erp.Pay.OrderPayChange")]
    public class OrderPayChangedEto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
