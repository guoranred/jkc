using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    [EventName("Erp.Injection.OfferChange")]
    public class OfferInjectionEto : OrderEventBaseDto
    { /// <summary>
      /// 对外报价
      /// </summary>
        public decimal SellingMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OfferInjectionEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="sellingMoney">对外报价</param>
        public OfferInjectionEto(string orderNo, decimal sellingMoney)
        {
            OrderNo = orderNo;
            SellingMoney = sellingMoney;
        }
    }
}
