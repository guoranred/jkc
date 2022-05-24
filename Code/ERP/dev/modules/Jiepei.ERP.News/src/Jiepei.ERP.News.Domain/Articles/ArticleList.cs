using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.News
{
    /// <summary>
    /// 文章列表
    /// </summary>
    public class ArticleList : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 渠道ID
        /// </summary>
        public virtual Guid? ChannelId { get; set; }

        /// <summary>
        ///标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 所属栏目名称
        /// </summary>
        public virtual string ColumnType { get; set; }


        /// <summary>
        /// 所属栏目ID
        /// </summary>
        public virtual Guid ColumnTypeId { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public virtual string ImgPath { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public virtual string Introduce { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public virtual bool ReleaseStatus { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool? IsDel { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsSetTop { get; set; }


        /// <summary>
        /// 文章标签
        /// </summary>
        public string Tag { get; set; }


        public Guid? TenantId => throw new NotImplementedException();

        public ArticleList() { }

        public ArticleList(Guid? channelId, string title, string columnType, Guid columnTypeId, string imgPath, string introduce, string author, DateTime? releaseTime, bool releaseStatus, bool? isDel, string content, int sort, bool isSetTop, string tag)
        {
            ChannelId = channelId;
            Title = title;
            ColumnType = columnType;
            ColumnTypeId = columnTypeId;
            ImgPath = imgPath;
            Introduce = introduce;
            Author = author;
            ReleaseTime = releaseTime;
            ReleaseStatus = releaseStatus;
            IsDel = isDel;
            Content = content;
            Sort = sort;
            IsSetTop = isSetTop;
            Tag = tag;
        }
    }
}
