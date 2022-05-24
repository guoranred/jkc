using System;

namespace Jiepei.ERP.Orders.Orders
{
    [Serializable]
    public class OfferOrderEto
    {
        public Guid OrderId { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public virtual decimal Cost { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public virtual decimal SellingPrice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public virtual decimal ShipPrice { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal DiscountMoney { get; set; }
        public OfferOrderEto(Guid orderId, decimal cost, decimal sellingPrice, decimal shipPrice, decimal discountMoney)
        {
            OrderId = orderId;
            Cost = cost;
            SellingPrice = sellingPrice;
            ShipPrice = shipPrice;
            DiscountMoney = discountMoney;
        }
    }
}
