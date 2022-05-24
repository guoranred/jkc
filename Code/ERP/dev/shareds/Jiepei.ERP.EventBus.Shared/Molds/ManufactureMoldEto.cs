using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Molds
{
    /// <summary>
    /// 模具订单投产 事件总线ETO
    /// </summary>
    [EventName("Erp.Mold.ManufactureChange")]
    public class ManufactureMoldEto : OrderEventBaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public ManufactureMoldEto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        public ManufactureMoldEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
