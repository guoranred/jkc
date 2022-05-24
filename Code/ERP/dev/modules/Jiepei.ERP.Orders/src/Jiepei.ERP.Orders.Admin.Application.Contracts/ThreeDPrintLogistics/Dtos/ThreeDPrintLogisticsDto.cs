using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos
{
    public class ThreeDPrintLogisticsDto : EntityDto<Guid>
    {
        public string OrderNo { get; set; }

        public string SupplierOrderCode { get; set; }

        public string Status { get; set; }

        public DateTime? InboundTime { get; set; }

        public DateTime? OutboundTime { get; set; }
    }
}
