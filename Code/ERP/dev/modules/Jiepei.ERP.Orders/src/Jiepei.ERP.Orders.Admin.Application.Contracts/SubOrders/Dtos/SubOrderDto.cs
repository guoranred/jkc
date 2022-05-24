using Jiepei.ERP.SubOrders;
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class SubOrderDto : CreationAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }

        /// <summary>
        /// 主订单编号
        /// </summary>
        public string MainOrderNo { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }

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
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 组织单元Id
        /// </summary>
        public Guid OrganizationUnitId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EnumSubOrderStatus Status { get; private set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string CourierCompany { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
