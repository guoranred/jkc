using Jiepei.ERP.Commons;
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EnumOrderStatus? Status { get; set; }

        ///// <summary>
        ///// 客户编号/手机号
        ///// </summary>
        //public string Customer { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public EnumOrigin? Origin { get; set; }

        /// <summary>
        /// 是否付款
        /// </summary>
        public bool? IsPay { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? StartPayDate { get; set; }
        public DateTime? EndPayDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? StartCreateDate { get; set; }
        public DateTime? EndCreateDate { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int? Type { get; set; }
    }
}
