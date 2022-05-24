using System;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class CustomerSubOrderThreeDItemDto
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
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// 可选类型 -- 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 材料id
        /// </summary>
        public Guid MaterialId { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 文件体积(立方毫米)
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 文件尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 后处理方式(表面处理)
        /// </summary>
        public string HandleMethod { get; set; }
        /// <summary>
        /// 后处理描述
        /// </summary>
        public string HandleMethodDesc { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal OrginalMoney { get; set; }
        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }
        /// <summary>
        /// 打印文件Id 
        /// </summary>
        public string SupplierFileId { get; set; }
        /// <summary>
        /// 预览文件ID 
        /// </summary>
        public string SupplierPreViewId { get; set; }
    }
}
