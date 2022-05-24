using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.News
{
    public class Banner : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 渠道ID
        /// </summary>
        public virtual Guid ChannelId { get; set; }

        /// <summary>
        /// 图片标题信息
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 图片链接地址
        /// </summary>
        public virtual string ImageUrl { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public virtual string RedirectUrl { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public virtual DateTime StartDate { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public virtual DateTime EndDate { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public virtual int SortOrder { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
