using System;

namespace Jiepei.ERP.News.Admin
{
    /// <summary>
    /// 文章修改Dto
    /// </summary>
    [Serializable]
    public class UpdateActicleListInput
    {
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 所属栏目
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 所属栏目Id
        /// </summary>
        public Guid ColumnTypeId { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public bool ReleaseStatus { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
       // public bool? IsDel { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsSetTop { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string ChannelId { get; set; }
    }
}
