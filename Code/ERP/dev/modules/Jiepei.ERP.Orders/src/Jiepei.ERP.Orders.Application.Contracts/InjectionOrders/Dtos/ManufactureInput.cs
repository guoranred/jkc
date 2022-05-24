using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class ManufactureInput
    {
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}