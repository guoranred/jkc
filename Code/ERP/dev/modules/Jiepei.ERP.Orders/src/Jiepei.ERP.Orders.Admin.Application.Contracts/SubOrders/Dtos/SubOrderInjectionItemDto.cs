﻿using Jiepei.ERP.Injections;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class SubOrderInjectionItemDto : ISubOrderItem
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

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
        /// 产品尺寸
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        public EnumInjectionMaterial Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public EnumInjectionSurface Surface { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
    }
}
