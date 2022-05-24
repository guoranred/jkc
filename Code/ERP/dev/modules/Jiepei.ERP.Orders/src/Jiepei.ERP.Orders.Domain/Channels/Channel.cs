using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Channels
{
    /// <summary>
    /// 渠道
    /// </summary>
    [Table("Channel")]
    public class Channel : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 渠道名称
        /// </summary>
        public virtual string ChannelName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable { get; set; }

        protected Channel() { }
    }
}
