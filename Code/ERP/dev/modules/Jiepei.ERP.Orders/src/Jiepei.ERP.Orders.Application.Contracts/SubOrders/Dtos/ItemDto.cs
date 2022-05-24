using Jiepei.ERP.Shared.Enums.SheetMetals;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class ItemDto
    {
        /// <summary>
        /// 产品文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 产品文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary> 
        /// 配件采购 0 不需要 1 自供 2 代采
        /// </summary>
        public EnumPurchasedParts PurchasedParts { get; set; }
        /// <summary>
        /// 是否需要设计 1是 0否
        /// </summary>
        public bool NeedDesign { get; set; }
    }
}
