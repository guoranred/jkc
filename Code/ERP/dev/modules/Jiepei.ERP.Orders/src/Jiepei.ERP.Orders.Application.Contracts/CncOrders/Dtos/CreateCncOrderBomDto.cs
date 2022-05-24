using Jiepei.ERP.Molds;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CreateCncOrderBomDto
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
        /// 数量
        /// </summary>
        public int? Qty { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
    }
}
