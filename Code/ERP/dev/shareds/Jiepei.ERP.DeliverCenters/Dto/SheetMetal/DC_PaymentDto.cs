using System;
using Jiepei.ERP.DeliverCentersClient.Enums;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_PaymentDto
    {

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidMoney { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipMoney { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public EnumDeliverCenterPayType PayType { get; set; }

        /// <summary>
        /// 支付渠道手续费
        /// </summary>
        public decimal PayChannelMoney { get; set; }
    }
}
