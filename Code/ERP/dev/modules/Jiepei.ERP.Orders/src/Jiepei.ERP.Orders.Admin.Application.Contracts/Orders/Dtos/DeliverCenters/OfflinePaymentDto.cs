using Jiepei.ERP.DeliverCentersClient.Enums;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos.DeliverCenters
{
    public class OfflinePaymentDto
    {
        /// <summary>
        /// 已付金额
        /// </summary>
        [Required]
        public decimal PaidMoney { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        [Required]
        public EnumDeliverCenterPayType PayType { get; set; }
    }
}
