using Jiepei.ERP.Shared.Enums.SheetMetals;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderSheetMetalItem : AuditedAggregateRoot<Guid>
    {
        protected SubOrderSheetMetalItem() { }
        /// <summary>
        /// 子订单Id
        /// </summary>
        public virtual Guid SubOrderId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public virtual string FilePath { get; set; }
        /// <summary>
        /// 产品套数
        /// </summary>
        public virtual int ProductNum { get; set; }
        /// <summary>
        /// 是否成套组装 1是0否
        /// </summary>
        public virtual bool AssembleType { get; set; }
        /// <summary>
        /// 是否需要设计 1是 0否
        /// </summary>
        public virtual bool NeedDesign { get; set; }
        /// <summary>
        /// 工艺参数字符串
        /// </summary>
        public virtual string ProcessParameters { get; set; }
        /// <summary> 
        /// 配件采购 0 不需要 1 自供 2 代采
        /// </summary>
        public EnumPurchasedParts PurchasedParts { get; set; }
        /// <summary>
        /// 3d文件预览地址
        /// </summary>
        public virtual string PreviewUrl { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public virtual string Thumbnail { get; set; }
        /// <summary>
        /// 打印文件Id 
        /// </summary>
        public virtual string SupplierFileId { get; set; }
        /// <summary>
        /// 预览文件ID 
        /// </summary>
        public virtual string SupplierPreViewId { get; set; }
        /// <summary>
        /// 文件 MD5
        /// </summary>
        public virtual string FileMD5 { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public virtual string MaterialName { get; set; }
        /// <summary>
        /// 表面处理
        /// </summary>
        public virtual string SurfaceProcess { get; set; }

        /// <summary>
        /// 产品备注
        /// </summary>
        public virtual string ProductRemark { get; set; }

        public void SetProductNum(int productNum)
        {
            ProductNum = productNum;
        }
    }
}
