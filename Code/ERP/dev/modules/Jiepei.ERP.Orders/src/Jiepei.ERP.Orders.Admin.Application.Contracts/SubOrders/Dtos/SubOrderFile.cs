using Jiepei.ERP.Shared.Enums.SheetMetals;

namespace Jiepei.ERP.Orders.Admin.Application.Contracts.SubOrders.Dtos
{
    public class SubOrderFile
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
        /// 是否需要设计 1是 0否
        /// </summary>
        public bool NeedDesign { get; set; }
        /// <summary> 
        /// 配件采购 0 不需要 1 自供 2 代采
        /// </summary>
        public EnumPurchasedParts PurchasedParts { get; set; }
        /// <summary>
        /// 工艺参数字符串
        /// </summary>
        public string ProcessParameters { get; set; }
    }
}
