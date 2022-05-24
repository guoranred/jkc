using System;

namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class ChannelMaterialListDto
    {
        /// <summary>
        /// 渠道
        /// </summary>
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 适用订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }
    }
}
