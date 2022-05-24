using Jiepei.ERP.Molds;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.MoldOrders.Dtos
{
    public class CreateMoldOrderDto
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public EnumMoldMaterial? Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public EnumMoldSurface? Surface { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public decimal? Long { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public decimal? Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get; set; }
    }
}
