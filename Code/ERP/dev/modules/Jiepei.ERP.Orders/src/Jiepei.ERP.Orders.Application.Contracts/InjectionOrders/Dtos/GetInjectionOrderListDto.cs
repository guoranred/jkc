using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class GetInjectionOrderListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
