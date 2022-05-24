namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class OrderBomCraftDto
    {
        /// <summary>
        ///零件ID
        /// </summary> 
        public int BomMaterialId { get; set; }

        /// <summary>
        ///选项名称 ，如 材质表面、表面处理等
        /// </summary> 
        public string ItemName { get; set; }

        /// <summary>
        ///工艺编号
        /// </summary> 
        public int CraftId { get; set; }

        /// <summary>
        ///工艺名称
        /// </summary> 
        public string CraftName { get; set; }

        /// <summary>
        ///数据类型
        /// </summary> 
        public string FieldType { get; set; }

        /// <summary>
        ///数据值
        /// </summary> 
        public string CraftValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string CraftUnit { get; set; }
    }
}
