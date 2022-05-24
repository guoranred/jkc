using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.News.Admin
{
    public class UpdateBannerManagementInput
    {
        public Guid ChannelId { get; set; }
        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Required(ErrorMessage = "图片地址不能为空")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        [Required(ErrorMessage = "有效时间不能为空")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        [Required(ErrorMessage = "有效时间不能为空")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 是否启用(默认启用)
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 排序(默认为0)
        /// </summary>
        [Range(0, 200)]
        public byte SortOrder { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
