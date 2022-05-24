using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class GetCncOrderListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
