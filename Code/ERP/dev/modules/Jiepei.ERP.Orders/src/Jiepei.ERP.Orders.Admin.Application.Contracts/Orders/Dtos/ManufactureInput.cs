using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin
{
    public class ManufactureInput
    {
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
