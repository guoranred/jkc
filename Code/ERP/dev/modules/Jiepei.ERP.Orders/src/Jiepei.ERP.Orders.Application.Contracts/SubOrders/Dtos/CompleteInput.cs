using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class CompleteInput
    {
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
