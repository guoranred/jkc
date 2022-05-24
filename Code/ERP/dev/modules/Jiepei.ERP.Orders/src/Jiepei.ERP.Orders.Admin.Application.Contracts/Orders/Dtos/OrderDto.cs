
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class OrderDto : FullAuditedEntityDto<Guid>
    {
        public string OrderNo { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// 关联外部订单编号
        /// </summary>
        public string ExterOrderNo { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        public string ApplicationArea { get; set; }

        /// <summary>
        /// 预计年使用量
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 渠道来源
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 总成本
        /// </summary>
        public decimal? TotalMoney { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal? SellingMoney { get; set; }

        /// <summary>
        /// 代付金额
        /// </summary>
        public decimal? PendingMoney { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal? PaidMoney { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType? OrderType { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public bool? IsPay { get; set; }
        /// <summary>
        /// 客服备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 交期天数
        /// </summary>
        public int? DeliveryDays { get; set; }
    }
}
