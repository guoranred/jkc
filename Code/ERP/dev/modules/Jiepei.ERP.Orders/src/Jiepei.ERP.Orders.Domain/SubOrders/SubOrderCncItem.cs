using Jiepei.ERP.Cncs;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderCncItem : AuditedAggregateRoot<Guid>
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
        public virtual EnumCncMaterial Material { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public virtual string MaterialName { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public virtual EnumCncSurface Surface { get; set; }
        /// <summary>
        /// 表面处理名称
        /// </summary>
        public virtual string SurfaceName { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        public virtual EnumApplicationArea ApplicationArea { get; set; }

       /// <summary>
        /// 表面处理等级名称
        /// </summary>
        public virtual string SurfaceLevelName { get; set; }

        /// <summary>
        /// 表面处理等级
        /// </summary>
        public virtual EnumCncSurfaceLevel SurfaceLevel { get; set; } 


        protected SubOrderCncItem() { }

        public SubOrderCncItem(Guid id
            , Guid subOrderId
            , string productName
            , string picture
            , string fileName
            , string filePath
            , string size
            , int quantity
            , EnumCncMaterial material
            , EnumCncSurface surface
            , EnumApplicationArea applicationArea
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
            Material = material;
            Surface = surface;
            ApplicationArea = applicationArea;
        }
    }
}
