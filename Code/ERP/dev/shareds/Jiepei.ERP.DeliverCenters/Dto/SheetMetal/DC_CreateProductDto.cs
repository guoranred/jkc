using Jiepei.BizMO.DeliverCenters.PrecisionMetal.Enums;
using System.Collections.Generic;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateProductDto
    {
        /// <summary>
        /// 预览图
        /// </summary>
        public string OrderFilePreview { get; set; }

        /// <summary>
        /// 上传图纸文件名称（用户原始文件名称）
        /// </summary>
        public string OrderFileName { get; set; }

        /// <summary>
        /// 上传图纸文件路径
        /// </summary>
        public string OrderFilePath { get; set; }

        /// <summary>
        /// 零件种类数量
        /// </summary>
        public int ProductBomNum { get; set; }

        /// <summary>
        /// 产品加工套数
        /// </summary>
        public int ProductNum { get; set; }

        /// <summary>
        /// 配件方式 0-无 1-代采购 2-自供  3-部分代采部分自供（供料方式）
        /// </summary>
        public EnumDeliverCenterProductFittingSourceType ProductFittingSourceType { get; set; }

        /// <summary>
        /// 是否成套组装 1是0否 默认false
        /// </summary>
        public bool ProductAssembleType { get; set; }

        /// <summary>
        /// 是否需要设计 1是 0否 默认false
        /// </summary>
        public bool ProductNeedDesign { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public List<DC_CreateBomDto> Bom { get; set; }
    }
}
