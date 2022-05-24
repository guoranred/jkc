using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class UpdateOrderInputDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 上传文件名称
        /// </summary>
        /// 
        [Required(AllowEmptyStrings = false, ErrorMessage = "请上传设计图纸")]
        public string ProductFileName { get; set; }

        /// <summary>
        /// 上传文件路径
        /// </summary>
        /// 

        [Required(AllowEmptyStrings = false, ErrorMessage = "请上传设计图纸")]
        public string ProductFilePath { get; set; }


        /// <summary>
        /// 3d文件预览地址
        /// </summary>
        public string PreviewUrl { get; set; }


        /// <summary>
        /// 3d文件缩略图地址
        /// </summary>
        public string Thumnail { get; set; }

    }
}
