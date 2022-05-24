using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ERP.Shared.Consumers.Orders.SubOrders;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderExtraAppService : IApplicationService
    {
        Task<bool> CancelExterAsync(MQSubOrderCancelDto input);
        Task PaymentExterAsync(MQSubOrderPaymentDto input);
        Task<bool> ReceiveExterAsync(MQSubOrderReceiveDto input);
        Task<bool> InjectionTaskExterAsync(MQ_Injection_OrderTaskDto input);
        Task<bool> MoldTaskExterAsync(MQ_Mold_OrderTaskDto input);
        Task<bool> CncTaskExterAsync(MQ_Cnc_OrderTaskDto input);
    }
}
