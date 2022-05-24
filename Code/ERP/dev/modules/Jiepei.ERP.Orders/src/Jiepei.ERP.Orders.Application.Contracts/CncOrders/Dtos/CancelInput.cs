using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CancelInput
    {
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Rremark { get; set; }
    }
}
