using Jiepei.ERP.Shared.Enums.SheetMetals;
using System;

namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    public class OrderInfoDto
    {
        /// <summary>
        ///订单用户ID
        /// </summary>  
        public int MemberId { get; set; } = 0;

        /// <summary>
        ///用户名称
        /// </summary>  
        public string MemberName { get; set; }

        /// <summaryDeliveryDate
        ///订单包
        /// </summary>  
        public string GroupNo { get; set; }

        /// <summary>
        ///订单编号
        /// </summary>  
        public string OrderNo { get; set; }

        /// <summary>
        ///订单别名（用户填写）
        /// </summary>  
        public string OrderName { get; set; }

        /// <summary>
        ///上传图纸文件名称
        /// </summary>  
        public string ProductFileName { get; set; }

        /// <summary>
        ///上传图纸文件路径
        /// </summary>  
        public string ProductFilePath { get; set; }

        /// <summary>
        ///零件种类数量
        /// </summary>   
        public int ProductBomNum { get; set; } = 0;

        /// <summary>
        ///（加工）产品套数
        /// </summary>  
        public int ProductNum { get; set; }

        /// <summary>
        ///配件方式 0-无 1-代采购 2-自供  3-部分代采部分自供
        /// </summary>  
        public EnumPurchasedParts ProductFittingSourceTypeStr { get; set; } = 0;

        /// <summary>
        ///配件清单文件
        /// </summary>  
        public string ProductFittingFilePath { get; set; }

        /// <summary>
        ///是否成套组装 1是0否
        /// </summary>  
        public bool ProductAssembleType { get; set; }

        /// <summary>
        ///是否需要设计 1是 0否
        /// </summary>  
        public bool ProductNeedDesign { get; set; }

        /// <summary>
        ///加工信息等备注
        /// </summary>  
        public string ProductRemark { get; set; }

        /// <summary>
        ///订单交期
        /// </summary> 
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// 前台计算价格
        /// </summary>
        public decimal FrontCalcPrice { get; set; }

        /// <summary>
        /// 跟单客服
        /// </summary>
        public string FollowAdminName { get; set; }

        /// <summary>
        /// 渠道来源代码
        /// </summary>
        public string OrderChannel { get; set; }

        /// <summary>
        /// 渠道来源名称
        /// </summary>
        public string OrderChannelName { get; set; }

        /// <summary>
        /// 计价方式
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; }

    }
}
