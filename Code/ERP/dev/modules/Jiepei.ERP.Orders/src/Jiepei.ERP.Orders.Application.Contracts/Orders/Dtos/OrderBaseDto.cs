using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class OrderBaseDto : EntityDto<Guid>
    {
        public string OrderNo { get; set; }
    }
}
