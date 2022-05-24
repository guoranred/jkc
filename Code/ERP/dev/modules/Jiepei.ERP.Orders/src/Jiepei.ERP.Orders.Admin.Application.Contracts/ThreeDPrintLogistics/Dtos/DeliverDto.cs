using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin
{
    public class DeliverDto
    {
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string CourierCompany { get; set; }

        public List<ChangeOutboundNumInput> Outbound { get; set; }
    }
}
