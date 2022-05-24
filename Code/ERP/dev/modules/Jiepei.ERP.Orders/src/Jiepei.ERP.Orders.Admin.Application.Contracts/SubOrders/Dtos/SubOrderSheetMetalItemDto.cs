using Jiepei.ERP.Shared.Enums.SheetMetals;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class SubOrderSheetMetalItemDto : ISubOrderItem
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 产品套数
        /// </summary>
        public int ProductNum { get; set; }
        /// <summary>
        /// 是否成套组装 1是0否
        /// </summary>
        public bool AssembleType { get; set; }
        /// <summary>
        /// 是否需要设计 1是 0否
        /// </summary>
        public bool NeedDesign { get; set; }
        /// <summary>
        /// 工艺参数字符串
        /// </summary>
        public string ProcessParameters { get; set; }
        /// <summary> 
        /// 配件采购 0 不需要 1 自供 2 代采
        /// </summary>
        public EnumPurchasedParts PurchasedParts { get; set; }
        /// <summary>
        /// 3d文件预览地址
        /// </summary>
        public string PreviewUrl { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// 打印文件Id 
        /// </summary>
        public string SupplierFileId { get; set; }
        /// <summary>
        /// 预览文件ID 
        /// </summary>
        public string SupplierPreViewId { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 表面处理
        /// </summary>
        public string SurfaceProcess { get; set; }

    }
}
