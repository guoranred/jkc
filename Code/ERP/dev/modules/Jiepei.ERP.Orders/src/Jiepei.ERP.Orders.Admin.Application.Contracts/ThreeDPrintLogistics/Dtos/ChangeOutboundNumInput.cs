using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class ChangeOutboundNumInput : EntityDto<Guid>
    {
        public int OutboundNum { get; set; }
    }
}
