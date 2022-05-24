using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class OfferInput
    {
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipPrice { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
