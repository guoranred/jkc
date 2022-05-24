using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders
{
    public class OrderCost : FullAuditedAggregateRoot<Guid>
    {
        protected OrderCost() { }


        public OrderCost(Guid id, string orderNo, decimal proMoney, decimal taxMoney, decimal taxPoint, decimal shipMoney)
        {
            Id = id;
            OrderNo = orderNo;
            ProMoney = proMoney;
            TaxMoney = taxMoney;
            TaxPoint = taxPoint;
            ShipMoney = shipMoney;
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 产品费
        /// </summary>
        public decimal ProMoney { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipMoney { get; set; }

        /// <summary>
        /// 税费
        /// </summary>
        public decimal TaxMoney { get; set; }

        /// <summary>
        /// 税点
        /// </summary>
        public decimal TaxPoint { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountMoney { get; set; }

        public void SetDiscountMoney(decimal discountMoney)
        {
            DiscountMoney = discountMoney;
        }

        public void SetProMoney(decimal proMoney)
        {
            ProMoney = proMoney;
        }

        public void SetShipMoney(decimal shipMoney)
        {
            ShipMoney = shipMoney;
        }
        public void SetTaxMoney(decimal taxMoney)
        {
            TaxMoney = taxMoney;
        }

        public void SetTaxPoint(decimal taxPoint)
        {
            TaxPoint = taxPoint;
        }


    }
}
