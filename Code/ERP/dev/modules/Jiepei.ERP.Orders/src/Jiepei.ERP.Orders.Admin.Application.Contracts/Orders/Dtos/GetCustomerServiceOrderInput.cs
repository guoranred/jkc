using Jiepei.ERP.Shared.Enums.Pays;
using Jiepei.ERP.SubOrders;
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class GetCustomerServiceOrderInput : PagedAndSortedResultRequestDto
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public Guid Channel { get; set; }
        public bool? IsPay { get; set; }
        public DateTime? StartPayDate { get; set; }
        public DateTime? EndPayDate { get; set; }
        public DateTime? StartCreateDate { get; set; }
        public DateTime? EndCreateDate { get; set; }
        public EnumOrderType? Type { get; set; }
        public EnumSubOrderStatus? OrderStatus { get; set; }
        public EnumPayType? PayType { get; set; }
        public string PayCode { get; set; }
    }
}
