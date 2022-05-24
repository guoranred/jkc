using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.CncOrders
{
    public class CncOrderBom : CreationAuditedAggregateRoot<Guid>
    {
        protected CncOrderBom() { }
        public CncOrderBom(
            Guid id,
            string orderNo,
            string proName,
            string picture,
            string fileName,
            string filePath,
            int? material,
            int? qty,
            string size,
            int? surface
            )
        {
            Id = id;
            OrderNo = orderNo;
            ProName = proName;
            Picture = picture;
            FileName = fileName;
            FilePath = filePath;
            Material = material;
            Qty = qty;
            Size = size;
            Surface = surface;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 产品文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 产品文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 产品材质(材料)
        /// </summary>
        public int? Material { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 表面处理
        /// </summary>
        public int? Surface { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
