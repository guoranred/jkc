using Jiepei.ERP.Orders.SubOrders.Dtos;
using System;
using System.Collections.Generic;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class CustomerOrderListDto
    {
        public Guid OrderId { get; set; }
        public string OrderNo { get; set; }

        public DateTime CreationTime { get; set; }
        public EnumOrderStatus Status { get; set; }
        public string StatusName { get; set; }
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 是否已支付
        /// </summary>
        public bool IsPay { get; set; }
        public List<CustomerSubOrderThreeDItemDto> Customer3DOrderExtraBomDtos { get; set; }
        public List<CustomerSubOrderCncItemDto> CustomerSubOrderCncItemDtos { get; set; }
        public List<CustomerSubOrderSheetMetalItemDto> CustomerSubOrderSheetMetalItemDtos { get; set; }
    }
}
