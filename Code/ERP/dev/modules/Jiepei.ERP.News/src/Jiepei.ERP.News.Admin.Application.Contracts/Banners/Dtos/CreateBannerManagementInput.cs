using System;

namespace Jiepei.ERP.News.Admin
{
    public class CreateBannerManagementInput
    { /// <summary>
      /// 渠道ID
      /// </summary>
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 图片标题信息
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片链接地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string RedirectUrl { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
