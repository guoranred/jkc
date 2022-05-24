using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CompleteInput
    {
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
