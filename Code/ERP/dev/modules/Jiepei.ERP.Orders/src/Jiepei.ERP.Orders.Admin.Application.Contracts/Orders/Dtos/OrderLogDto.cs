using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class OrderLogDto : CreationAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Content { get; set; }
    }
}
