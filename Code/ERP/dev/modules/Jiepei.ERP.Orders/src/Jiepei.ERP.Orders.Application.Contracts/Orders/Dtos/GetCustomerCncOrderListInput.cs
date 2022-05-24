using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class GetCustomerCncOrderListInput : PagedAndSortedResultRequestDto
    {
        public Guid ChannelId { get; set; }
        public EnumOrderStatus? Status { get; set; }
        public string OrderNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public EnumOrderType OrderType { get; set; }
    }
}
