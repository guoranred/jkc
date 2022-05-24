using System.Collections.Generic;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class OrderBomMaterialDto
    {
        /// <summary>
        ///编号
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        ///订单ID
        /// </summary> 
        public int OrderId { get; set; } = 0;

        /// <summary>
        ///零件名称
        /// </summary> 
        public string PartName { get; set; }

        /// <summary>
        ///长度（单位：毫米）
        /// </summary> 
        public decimal Length { get; set; }

        /// <summary>
        ///宽度
        /// </summary> 
        public decimal Width { get; set; }

        /// <summary>
        ///厚度
        /// </summary> 
        public string Height { get; set; }

        /// <summary>
        ///材料编号
        /// </summary> 
        public int MaterialId { get; set; }

        /// <summary>
        ///材料类别名称
        /// </summary> 
        public string MaterialCategoryName { get; set; }

        /// <summary>
        ///材料名称
        /// </summary> 
        public string MaterialName { get; set; }

        /// <summary>
        ///数量
        /// </summary> 
        public int Num { get; set; }

        /// <summary>
        ///特殊备注
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        ///零件总价格
        /// </summary> 
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 包含工艺
        /// </summary>
        public List<OrderBomCraftDto> OrderBomCraftDTOs { get; set; } = new();

        /// <summary>
        /// 包含辅料
        /// </summary>
        public List<OrderBomAccessoryDto> OrderBomAccessoryDTOs { get; set; } = new();
    }
}
