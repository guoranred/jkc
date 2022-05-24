using System;

namespace Jiepei.ERP.News
{
    public class GetBannerInfoOutputDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 图片标题
        /// </summary> 
        public virtual string Title { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual string ImageUrl { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public virtual string RedirectUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
