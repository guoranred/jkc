using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateBomDto
    {  

        /// <summary>
        /// 零件名称
        /// </summary>
        public string BomName { get; set; }

        /// <summary>
        /// 零件类型
        /// </summary>
        public int BomType { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// 厚度
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 材料编号
        /// </summary>
        public string MaterialId { get; set; }

        /// <summary>
        /// 材料类别名称
        /// </summary>
        public string MaterialCategoryName { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 单套数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public List<DC_CreateAttributeDto> Attribute { get; set; }

    }
}
