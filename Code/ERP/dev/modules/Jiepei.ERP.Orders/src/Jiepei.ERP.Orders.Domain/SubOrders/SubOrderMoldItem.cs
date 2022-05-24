using Jiepei.ERP.Molds;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderMoldItem : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 子订单Id
        /// </summary>
        public virtual Guid SubOrderId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProductName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public virtual string Picture { get; set; }

        /// <summary>
        /// 产品文件名称
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 产品文件路径
        /// </summary>
        public virtual string FilePath { get; set; }

        /// <summary>
        /// 产品尺寸
        /// </summary>
        public virtual string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Quantity { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public virtual EnumMoldMaterial Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public virtual EnumMoldSurface Surface { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        public virtual EnumApplicationArea ApplicationArea { get; set; }

        /// <summary>
        /// 预计年使用量
        /// </summary>
        public virtual EnumUsage Usage { get; set; }

        protected SubOrderMoldItem() { }

        public SubOrderMoldItem(Guid id
            , Guid subOrderId
            , string productName
            , string picture
            , string fileName
            , string filePath
            , string size
            , int quantity
            , string color
            , EnumMoldMaterial material
            , EnumMoldSurface surface
            , EnumApplicationArea applicationArea
            , EnumUsage usage
            ) : base(id)
        {
            Id = id;
            SubOrderId = subOrderId;
            ProductName = productName;
            Picture = picture;
            FileName = fileName;
            FilePath = filePath;
            Size = size;
            Quantity = quantity;
            Color = color;
            Material = material;
            Surface = surface;
            ApplicationArea = applicationArea;
            Usage = usage;
        }

        public void SetFilePath(string filePath)
        {
            FilePath = filePath;
        }

        public void SetFileName(string fileName)
        {
            FileName = fileName;
        }

        public void SetPicture(string picture)
        {
            Picture = picture;
        }

    }
}
