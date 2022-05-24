using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Orders.EventHandlers
{
    public class DeliverOrderEventHandler : ILocalEventHandler<DeliverOrderEto>, ITransientDependency
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public DeliverOrderEventHandler(IOrderManager orderManager, IOrderRepository orderRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork(true)]
        public async Task HandleEventAsync(DeliverOrderEto eventData)
        {
            var order = await _orderRepository.FindAsync(eventData.OrderId);
            await _orderManager.DeliverAsync(order, eventData.TrackingNo, eventData.CourierCompany);
        }
    }
}
