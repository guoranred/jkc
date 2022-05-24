using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin
{
    public class SubOrderThreeDItemDto : EntityDto<Guid>
    {
        public Guid SubOrderId { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 产品文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public int InboundNum { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public int OutboundNum { get; set; }
        /// <summary>
        /// 下单数量
        /// </summary>
        public int OrderNum { get; set; }
    }
}
