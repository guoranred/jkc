using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class OrderBaseDto : EntityDto<Guid>
    {
        public string OrderNo { get; set; }
    }
}
