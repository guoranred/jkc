using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ERP.Orders.MoldOrders.Dtos
{
    public class SearchInput
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 渠道来源
        /// </summary>
        public int Origin { get; set; }
    }
}
