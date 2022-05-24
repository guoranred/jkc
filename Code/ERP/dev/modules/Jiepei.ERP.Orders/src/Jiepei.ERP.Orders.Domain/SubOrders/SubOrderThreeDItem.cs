using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderThreeDItem : AuditedAggregateRoot<Guid>
    {
        protected SubOrderThreeDItem() { }
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
        /// 缩略图
        /// </summary>
        public virtual string Thumbnail { get; set; }
        /// <summary>
        /// 可选类型 -- 颜色
        /// </summary>
        public virtual string Color { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public virtual string MaterialName { get; set; }

        /// <summary>
        /// 材料id
        /// </summary>
        public virtual Guid MaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 文件体积(立方毫米)
        /// </summary>
        public virtual decimal Volume { get; set; }
        /// <summary>
        /// 文件尺寸
        /// </summary>
        public virtual string Size { get; set; }
        /// <summary>
        /// 支撑体积(立方毫米) 【需要软件计算，目前未使用到】
        /// </summary>
        public virtual decimal SupportVolume { get; set; }
        /// <summary>
        /// 后处理方式(表面处理)
        /// </summary>
        public virtual string HandleMethod { get; set; }
        /// <summary>
        /// 后处理描述
        /// </summary>
        public virtual string HandleMethodDesc { get; set; }
        /// <summary>
        /// 后处理费用
        /// </summary>
        public virtual decimal HandleFee { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal Price { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public virtual decimal OrginalMoney { get; set; }
        /// <summary>
        /// 交期天数
        /// </summary>
        public virtual int DeliveryDays { get; set; }
        /// <summary>
        /// 打印文件Id 
        /// </summary>
        public virtual string SupplierFileId { get; set; }
        /// <summary>
        /// 预览文件ID 
        /// </summary>
        public virtual string SupplierPreViewId { get; set; }
        /// <summary>
        /// 供应商订单编号
        /// </summary>
        public virtual string SupplierOrderCode { get; set; }
        /// <summary>
        /// 文件 MD5
        /// </summary>
        public virtual string FileMD5 { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public virtual int InboundNum { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public virtual int OutboundNum { get; set; }
        /// <summary>
        /// 供应商 Id
        /// </summary>
        public virtual Guid SupplierId { get; set; }

        public void SetSubOrderId(Guid subOrderId)
        {
            SubOrderId = subOrderId;
        }
        public void SetHandleFee(decimal handleFee)
        {
            HandleFee = handleFee;
        }
        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public void SetOrginalMoney(decimal orginalMoney)
        {
            OrginalMoney = orginalMoney;
        }
        public void SetDeliveryDays(int deliveryDays)
        {
            DeliveryDays = deliveryDays;
        }

    }
}