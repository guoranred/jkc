using Jiepei.ERP.DeliverCentersClient.Enums;
using System;

namespace Jiepei.ERP.DeliverCentersClient.Dto
{
    public class OfflinePaymentRequest
    {
        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidMoney { get; protected set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime PayTime { get; protected set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public EnumDeliverCenterPayType PayType { get; protected set; }

        public OfflinePaymentRequest(decimal paidMoney, EnumDeliverCenterPayType payType)
        {
            PaidMoney = paidMoney;
            PayTime = DateTime.Now;
            PayType = payType;
        }
    }
}
