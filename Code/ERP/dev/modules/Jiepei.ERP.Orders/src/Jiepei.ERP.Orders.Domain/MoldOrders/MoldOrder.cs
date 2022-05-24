using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public class MoldOrder : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 主订单号
        /// </summary>
        public string MainOrderNo { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public int? Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public int? Surface { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public decimal? Long { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public decimal? Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; protected set; }

        public Guid? TenantId { get; protected set; }

        protected MoldOrder() { }

        public MoldOrder(Guid id,
            Guid? tenantId,
            string orderNo,
            string proName,
            string picture,
            string fileName,
            string filePath,
            string color,
            int? material,
            int? surface,
            int? qty,
            decimal? @long,
            decimal? width,
            decimal? height,
            decimal? weight
            )
        {
            Id = id;
            TenantId = tenantId;
            OrderNo = orderNo;
            ProName = proName;
            Picture = picture;
            FileName = fileName;
            FilePath = filePath;
            Color = color;
            Material = material;
            Surface = surface;
            Qty = qty;
            Long = @long;
            Width = width;
            Height = height;
            Weight = weight;
        }

        public void SetStatus(int status)
        {
            Status = status;
        }

        public void SetOrderNo(string orderNo)
        {
            OrderNo = orderNo;
        }

        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public void SetMainOrderNo(string value)
        {
            MainOrderNo = value;
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
