using System;
using System.Collections.Generic;
using System.Text;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CncOrderBomDto
    {
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
        public string Material { get; set; }

        /// <summary>
        /// 表面处理
        /// </summary>
        public string Surface { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///数量
        /// </summary>
        public string Qty { get; set; }
        /// <summary>
        ///状态
        /// </summary>
        public string States { get; set; }
    }
}
