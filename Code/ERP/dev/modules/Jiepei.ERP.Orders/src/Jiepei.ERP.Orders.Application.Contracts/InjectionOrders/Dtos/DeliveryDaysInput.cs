using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
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