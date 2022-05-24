using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.MoldOrders.Dtos
{
    public class ManufactureInput
    {
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
