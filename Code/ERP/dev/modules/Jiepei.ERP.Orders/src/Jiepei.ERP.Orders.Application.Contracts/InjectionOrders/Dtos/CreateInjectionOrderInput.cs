using Jiepei.ERP.Injections;

namespace Jiepei.ERP.Orders.InjectionOrders.Dtos
{
    public class CreateInjectionOrderInput
    {
        /// <summary>
        /// 模具关联订单号
        /// </summary>
        public string MoldOrderNo { get; set; }
        /// <summary>
        /// 包装方式
        /// </summary>
        public EnumInjectionOrderPackMethod? PackMethod { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get; set; }
    }
}
