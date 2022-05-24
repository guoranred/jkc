using Jiepei.ERP.Orders.SubOrders.Dtos;
using System;
using System.Collections.Generic;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class CustomerCncOrderDto
    {
        public Guid OrderId { get; set; }
        public string OrderNo { get; set; }

        public DateTime CreationTime { get; set; }
        public EnumOrderStatus Status { get; set; }
        public string StatusName { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// 审核不通过原因
        /// </summary>
        public string CheckNoPassReason { get; set; }
        /// <summary>
        /// 交期时间
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }
        public int MyProperty { get; set; }
        public List<CreateSubOrderCncItemDto> CreateSubOrderSheetMetalItemDtos { get; set; }
        public List<DeliveryDto> DeliveryDto { get; set; }

        public List<CostDto> CostDto { get; set; }
    }
}
