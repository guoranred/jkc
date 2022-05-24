using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class CheckInput
    {
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPassed { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
