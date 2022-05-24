using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Cncs
{
    [EventName("Erp.Cnc.OfferChange")]
    public class OfferCncEto : OrderEventBaseDto
    { /// <summary>
      /// 对外报价
      /// </summary>
        public decimal SellingMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OfferCncEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="sellingMoney">对外报价</param>
        public OfferCncEto(string orderNo, decimal sellingMoney)
        {
            OrderNo = orderNo;
            SellingMoney = sellingMoney;
        }
    }
}
