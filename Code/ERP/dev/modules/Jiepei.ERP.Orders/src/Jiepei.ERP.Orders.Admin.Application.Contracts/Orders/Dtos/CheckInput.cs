using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class CheckInput
    {
        /// <summary>
        /// 是否通过
        /// </summary>
        [Required]
        public bool IsPassed { get; set; }
        /// <summary>
        /// 供应商 Id
        /// </summary>
        [Required]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
