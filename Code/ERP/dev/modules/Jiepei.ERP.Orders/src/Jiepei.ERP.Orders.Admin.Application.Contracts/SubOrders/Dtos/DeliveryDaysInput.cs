using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class DeliveryDaysInput
    {
        /// <summary>
        /// 交期
        /// </summary>
        [Required]
        [Range(3, 1000000000)]
        public int DeliveryDays { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Rremark { get; set; }
    }
}