using Jiepei.ERP.SubOrders;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class GetThreeDPrintLogisticsInput : PagedAndSortedResultRequestDto
    {
        public string OrderNo { get; set; }
        public string TrackingNo { get; set; }
        public EnumSubOrderStatus? Status { get; set; }
    }
}
