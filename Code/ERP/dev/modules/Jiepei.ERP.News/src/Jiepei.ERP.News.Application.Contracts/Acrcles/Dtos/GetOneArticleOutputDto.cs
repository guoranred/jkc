using System;

namespace Jiepei.ERP.News
{
    public class GetOneArticleOutputDto
    {
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }


        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }


        /// <summary>
        /// 作者
        /// </summary>
        public virtual string Author { get; set; }


        /// <summary>
        ///标题
        /// </summary>
        public virtual string Title { get; set; }
    }
}
