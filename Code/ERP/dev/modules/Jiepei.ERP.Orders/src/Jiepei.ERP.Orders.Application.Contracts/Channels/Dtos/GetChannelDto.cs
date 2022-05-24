using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Channels.Dtos
{
    public class GetChannelDto : EntityDto<Guid>
    {
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
