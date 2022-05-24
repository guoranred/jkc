using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;


namespace Jiepei.ERP.Orders.Admin
{
    public class CustomerServiceOrderDto : CreationAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public string OrderNo { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string SupplierOrderCodes { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellingPrice { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 客服备注
        /// </summary>
        public string CustomerServiceRemark { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public List<OrderItemDto> OrderItems { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public Guid ChannelId { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 会员联系方式
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public bool IsPay { get; set; }
        /// <summary>
        /// 客服人员
        /// </summary>
        public Guid? CustomerService { get; set; }

        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 支付渠道
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayCode { get; set; }
    }
}
