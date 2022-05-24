using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class CostDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 产品费
        /// </summary>
        public decimal? ProMoney { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal? ShipMoney { get; set; }

        /// <summary>
        /// 税费
        /// </summary>
        public decimal? TaxMoney { get; set; }

        /// <summary>
        /// 税点
        /// </summary>
        public decimal? TaxPoint { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal? DiscountMoney { get; set; }
    }
}
