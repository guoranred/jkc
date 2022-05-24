using Jiepei.ERP.Molds;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CncOrderDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string OrderNo { get; set; }

        public List<CncOrderBomDto> CncOrderBomDto { get; set; }
    }
}
