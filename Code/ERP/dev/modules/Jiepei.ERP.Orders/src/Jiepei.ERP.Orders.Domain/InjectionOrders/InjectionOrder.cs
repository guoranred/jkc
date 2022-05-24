using Jiepei.ERP.Injections;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public class InjectionOrder : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        protected InjectionOrder() { }
        public InjectionOrder(
            string moldOrderNo,
            string proName,
            string picture,
            string fileName,
            string filePath,
            int? material,
            int? qty,
            string size,
            int? surface,
            int? packMethod
            )
        {
            MoldOrderNo = moldOrderNo;
            ProName = proName;
            Picture = picture;
            FileName = fileName;
            FilePath = filePath;
            Material = material;
            Qty = qty;
            Size = size;
            Surface = surface;
            PackMethod = packMethod;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; protected set; }
        /// <summary>
        /// 主订单号
        /// </summary>
        public string MainOrderNo { get; protected set; }
        /// <summary>
        /// 模具关联订单号
        /// </summary>
        public string MoldOrderNo { get; protected set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? Status { get; protected set; }
        /// <summary>
        /// 特殊备注
        /// </summary>
        public string Remark { get; protected set; }
        /// <summary>
        /// 包装方式
        /// </summary>
        public int? PackMethod { get; protected set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; protected set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get; protected set; }
        /// <summary>
        /// 产品文件名称
        /// </summary>
        public string FileName { get; protected set; }
        /// <summary>
        /// 产品文件路径
        /// </summary>
        public string FilePath { get; protected set; }
        /// <summary>
        /// 产品材质(材料)
        /// </summary>
        public int? Material { get; protected set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get; protected set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; protected set; }
        /// <summary>
        /// 表面处理
        /// </summary>
        public int? Surface { get; protected set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; protected set; }

        /// <summary>
        /// 客服备注
        /// </summary>
        public string CustomerRemark { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public void SetOrderNo(string orderNo)
        {
            OrderNo = orderNo;
        }

        public void SetStatus(int status)
        {
            Status = status;
        }

        public void SetMainOrderNo(string mainOrderNo)
        {
            MainOrderNo = mainOrderNo;
        }

        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public void SetMoldOrderNo(string moldOrderNo)
        {
            MoldOrderNo = moldOrderNo;
        }
    }
}