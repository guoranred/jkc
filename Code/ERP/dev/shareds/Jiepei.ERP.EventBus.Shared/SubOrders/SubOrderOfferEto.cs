using Jiepei.ERP.Orders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.OfferChange")]
    public class SubOrderOfferEto : OrderEventBaseDto
    { /// <summary>
      /// 对外报价
      /// </summary>
        public decimal SellingMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SubOrderOfferEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="sellingMoney">对外报价</param>
        /// <param name="orderTyp"></param>
        public SubOrderOfferEto(string orderNo, decimal sellingMoney, EnumOrderType orderTyp)
        {
            OrderNo = orderNo;
            SellingMoney = sellingMoney;
            OrderType = orderTyp;
        }
    }
}
