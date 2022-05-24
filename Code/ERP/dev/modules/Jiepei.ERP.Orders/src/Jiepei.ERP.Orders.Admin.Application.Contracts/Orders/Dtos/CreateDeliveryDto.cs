using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin
{
    public class CreateDeliveryDto
    {
        /// <summary>
        /// 总重量
        /// </summary>
        [Required]
        public decimal? Weight { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货公司名
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ReceiverCompany { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ReceiverTel { get; set; }

        /// <summary>
        /// 订单联系人
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string OrderContactName { get; set; }

        /// <summary>
        /// 订单联系人手机号
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string OrderContactMobile { get; set; }

        /// <summary>
        /// 订单联系人QQ
        /// </summary>
        [MaxLength(20)]
        public string OrderContactQQ { get; set; }
    }
}
