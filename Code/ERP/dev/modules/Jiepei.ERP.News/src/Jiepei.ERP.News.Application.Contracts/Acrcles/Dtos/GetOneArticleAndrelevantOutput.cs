using System;
using System.Collections.Generic;

namespace Jiepei.ERP.News
{
    public class GetOneArticleAndrelevantOutput
    {

        /// <summary>
        /// 当前文章正文
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


        /// <summary>
        /// 相关推荐
        /// </summary>
        public List<ArticleListOutput> OtherArticles { get; set; }

    }
}
