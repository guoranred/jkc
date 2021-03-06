using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.MoldOrders.Dtos
{
    public class OfferInput
    {
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal? TotalMoney { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal? SellingMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
