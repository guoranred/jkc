
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class OrderDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 渠道订单号
        /// </summary>
        public string ChannelOrderNo { get; set; }

        /// <summary>
        /// 渠道用户Id
        /// </summary>
        public string ChannelUserId { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellingPrice { get; set; }

        /// <summary>
        /// 代付金额
        /// </summary>
        public decimal PendingMoney { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidMoney { get; set; }

        /// <summary>
        /// 组织单元Id
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EnumOrderStatus Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public byte PayMode { get; set; }

        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }

        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 客户备注
        /// </summary>
        public string CustomerRemark { get; set; }

        /// <summary>
        /// 客服备注
        /// </summary>
        public string CustomerServiceRemark { get; set; }

        /// <summary>
        /// 客服人员
        /// </summary>
        public Guid? CustomerService { get; set; }

        /// <summary>
        /// 工程师
        /// </summary>
        public Guid? Engineer { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string CourierCompany { get; set; }
    }
}
