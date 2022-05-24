using Jiepei.ERP.Injections;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class InjectionOrderDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get;  set; }
        /// <summary>
        /// 主订单号
        /// </summary>
        public string MainOrderNo { get;  set; }
        /// <summary>
        /// 模具关联订单号
        /// </summary>
        public string MoldOrderNo { get;  set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get;  set; }
        /// <summary>
        /// 特殊备注
        /// </summary>
        public string Remark { get;  set; }
        /// <summary>
        /// 包装方式
        /// </summary>
        public string PackMethod { get;  set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get;  set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture { get;  set; }
        /// <summary>
        /// 产品文件名称
        /// </summary>
        public string FileName { get;  set; }
        /// <summary>
        /// 产品文件路径
        /// </summary>
        public string FilePath { get;  set; }
        /// <summary>
        /// 产品材质(材料)
        /// </summary>
        public string Material { get;  set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get;  set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get;  set; }
        /// <summary>
        /// 表面处理
        /// </summary>
        public string Surface { get;  set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get;  set; }

        /// <summary>
        /// 客服备注
        /// </summary>
        public string CustomerRemark { get;  set; }
    }
}
