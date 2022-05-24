using System.ComponentModel;

namespace Jiepei.ERP.Members.Enums
{
    public enum CustomerServiceTypeEnum
    {
        /// <summary>
        /// 客服
        /// </summary>
        [Description("客服")]
        CustomerService = 0,

        /// <summary>
        /// 业务员
        /// </summary>
        [Description("业务员")]
        SalesMan = 1,

        /// <summary>
        /// 推广渠道
        /// </summary>
        [Description("推广渠道")]
        PromoChannel = 2
    }
}
