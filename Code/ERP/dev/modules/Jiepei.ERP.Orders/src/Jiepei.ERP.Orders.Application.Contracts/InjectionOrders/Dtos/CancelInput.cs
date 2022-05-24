using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class CancelInput
    {        /// <summary>
             /// 备注
             /// </summary>
        [MaxLength(500)]
        public string Rremark { get; set; }
    }
}
