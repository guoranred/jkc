using System.Collections.Generic;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class ProductPriceInputDto
    {

        /// <summary>
        ///产品套数
        /// </summary>
        public int ProductNum { get; set; }

        /// <summary>
        /// 零件列表
        /// </summary>
        public List<ProductPriceBomVO> BomList { get; set; }

        /// <summary>
        /// 是否后台核价 {与前台计价的区别 价格不同、工艺更多}
        /// </summary>
        public bool IsBackPricing { get; set; } = false;

        public int? PricingCompany { get; set; }
    }

    public class ProductPriceBomVO
    {

        /// <summary>
        ///零件名称
        /// </summary> 
        public string PartName { get; set; }

        /// <summary>
        ///长度（单位：毫米）
        /// </summary> 
        public decimal Length { get; set; }

        /// <summary>
        ///宽度（单位：毫米）
        /// </summary> 
        public decimal Width { get; set; }

        /// <summary>
        ///厚度（单位：毫米）
        /// </summary> 
        public decimal Height { get; set; }

        /// <summary>
        ///材料编号
        /// </summary> 
        public int MaterialId { get; set; }

        /// <summary>
        ///数量
        /// </summary> 
        public int Num { get; set; }

        /// <summary>
        ///零件特殊备注
        /// </summary> 
        public string Remark { get; set; }

        //todo 后续可以简单点 工艺编号-该数量
        /// <summary>
        /// 工艺列表
        /// </summary>
        public List<ProductPriceBomCraftVO> BomCraftList { get; set; }

    }

    public class ProductPriceBomCraftVO
    {
        /// <summary>
        ///工艺编号
        /// </summary>
        public int CraftId { get; set; }

        /// <summary>
        /// 数量（工艺类型为输入）,如果是选择(单选或复选)类型，则为空
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string FiledType { get; set; }


    }

}
