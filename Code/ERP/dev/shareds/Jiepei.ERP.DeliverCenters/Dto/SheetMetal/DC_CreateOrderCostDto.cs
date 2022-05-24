
namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateOrderCostDto
    {
        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipMoney { get; set; }

        /// <summary>
        /// 税点
        /// </summary>
        public int TaxPoint { get; set; }

        /// <summary>
        /// 白条加点
        /// </summary>
        public int BaitiaoPoint { get; set; }

        /// <summary>
        /// 优惠总金额
        /// </summary>
        public decimal PreferentialMoney { get; set; }
    }
}
