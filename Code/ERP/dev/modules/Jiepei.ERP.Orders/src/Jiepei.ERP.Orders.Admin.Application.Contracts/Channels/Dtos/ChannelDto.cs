using System;

namespace Jiepei.ERP.Orders.Channels.Dtos
{
    public class ChannelDto
    {
        /// <summary>
        /// 渠道id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 渠道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
