using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.MoldOrders.Dtos
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
