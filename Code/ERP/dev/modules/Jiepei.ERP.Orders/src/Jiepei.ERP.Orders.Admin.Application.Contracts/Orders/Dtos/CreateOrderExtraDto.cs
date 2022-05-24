
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace Jiepei.ERP.Orders.Admin
{
    public class CreateOrderExtraDto : ExtensibleObject
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        [Required]
        [EnumDataType(typeof(EnumOrderType))]
        public EnumOrderType OrderType { get; set; }

        /// <summary>
        /// 关联外部订单编号
        /// </summary>
        [MaxLength(64)]
        public string ChannelOrderNo { get; set; }

        /// <summary>
        /// 渠道来源
        /// </summary>
        [Required]
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 组织单元Id 
        /// </summary>
        public Guid OrganizationUnitId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        [Required]
        [EnumDataType(typeof(EnumApplicationArea))]
        public EnumApplicationArea? ApplicationArea { get; set; }

        /// <summary>
        /// 预计年使用量
        /// </summary>
        [Required]
        [EnumDataType(typeof(EnumUsage))]
        public EnumUsage? Usage { get; set; }

        /// <summary>
        /// 配送信息
        /// </summary>
        public CreateDeliveryDto DeliveryInfo { get; set; }
    }
}
