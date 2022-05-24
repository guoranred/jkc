using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class UpdateOrderStopExecDto
    {
        public UpdateOrderStopExecDto()
        {

        }

        public UpdateOrderStopExecDto(string orderNo, bool isStopExec = false)
        {
            this.OrderNo = orderNo;
            this.IsStopExec = isStopExec;
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        public string OrderNo { get; set; }

        /// <summary>
        /// 是否停止执行  确认下单之后,应该是正常执行还是暂停执行(暂停执行则订单生产会暂停)
        /// </summary>
        public bool IsStopExec { get; set; } = false;
    }
}
