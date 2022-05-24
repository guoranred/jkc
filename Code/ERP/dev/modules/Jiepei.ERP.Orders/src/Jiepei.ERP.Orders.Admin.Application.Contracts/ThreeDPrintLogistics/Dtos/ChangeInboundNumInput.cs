using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class ChangeInboundNumInput : EntityDto<Guid>
    {
        public int InboundNum { get; set; }
    }
}
