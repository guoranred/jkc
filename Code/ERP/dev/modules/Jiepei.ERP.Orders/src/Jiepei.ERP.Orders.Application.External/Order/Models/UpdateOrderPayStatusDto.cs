using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class UpdateOrderPayStatusDto
    {
        public UpdateOrderPayStatusDto()
        {

        }

        public UpdateOrderPayStatusDto(string groupNo, bool isPay = true)
        {
            this.GroupNo = groupNo;
            this.IsPay = isPay;
        }

        /// <summary>
        /// 订单包编号
        /// </summary>
        [Required]
        public string GroupNo { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public bool IsPay { get; set; } = true;
    }
}
