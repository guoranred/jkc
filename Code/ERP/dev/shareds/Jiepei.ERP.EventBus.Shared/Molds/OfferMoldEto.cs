using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Molds
{
    /// <summary>
    /// 模具订单报价 事件总线ETO
    /// </summary>
    [EventName("Erp.Mold.OfferChange")]
    public class OfferMoldEto : OrderEventBaseDto
    {
        /// <summary>
        /// 对外报价
        /// </summary>
        public decimal SellingMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OfferMoldEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="sellingMoney">对外报价</param>
        public OfferMoldEto(string orderNo, decimal sellingMoney)
        {
            OrderNo = orderNo;
            SellingMoney = sellingMoney;
        }
    }
}
