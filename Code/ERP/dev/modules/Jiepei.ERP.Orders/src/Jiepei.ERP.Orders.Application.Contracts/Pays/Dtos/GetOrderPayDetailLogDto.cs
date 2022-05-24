using Jiepei.ERP.Shared.Enums.Pays;
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Pays.Dtos
{
    public class GetOrderPayDetailLogDto : EntityDto<Guid>
    {
        /// <summary>
        /// 外键
        /// </summary>
        public Guid PayLogId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SellingMoney { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 流水类型
        /// </summary>
        public EnumOrderFlowType FlowType { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? IsSuccess { get; set; }
    }
}
