using System;
using System.Collections.Generic;

namespace Jiepei.ERP.News
{
    public class GetColumnTypesOutputDto
    {

        public Guid Id { get; set; }


        /// <summary>
        /// 栏目名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 栏目别名
        /// </summary>
        public virtual string Alias { get; set; }


        /// <summary>
        /// logo图片
        /// </summary>
        public virtual string LogoImage { get; set; }



        public List<ArticleListOutput> ArticleLists { get; set; }

    }



    public class ArticleListOutput
    {

        public Guid ColumnTypeId { get; set; }

        public Guid Id { get; set; }
        /// <summary>
        ///标题
        /// </summary>
        public virtual string Title { get; set; }


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
        /// 文章标签
        /// </summary>
        public string Tag { get; set; }
    }




}
