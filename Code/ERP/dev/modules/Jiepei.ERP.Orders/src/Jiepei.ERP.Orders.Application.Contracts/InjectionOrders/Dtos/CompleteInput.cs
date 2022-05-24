using Jiepei.ERP.Injections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class CompleteInput
    {
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
