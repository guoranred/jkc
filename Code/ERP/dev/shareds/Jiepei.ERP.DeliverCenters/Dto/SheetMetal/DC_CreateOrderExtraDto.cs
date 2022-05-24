
using Jiepei.ERP.DeliverCentersClient.Enums;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateOrderExtraDto
    {
        /// <summary>
        /// 是否淘宝官网
        /// </summary>
        public int IsTaoBao { get; set; }
        /// <summary>
        /// 应用领域
        /// </summary>
        public EnumDeliverCenterApplicationArea? ApplicationArea { get; set; }
        /// <summary>
        /// 预计年使用量 
        /// </summary>
        public EnumDeliverCenterUsage? Usage { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 客户备注 
        /// </summary>
        public string MemberRemark { get; set; }
    }
}
