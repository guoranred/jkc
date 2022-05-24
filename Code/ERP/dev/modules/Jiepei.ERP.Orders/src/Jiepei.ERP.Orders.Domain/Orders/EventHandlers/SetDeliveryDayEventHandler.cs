using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Orders.EventHandlers
{
    public class SetDeliveryDayEventHandler : ILocalEventHandler<SetDeliveryDayEto>,ITransientDependency
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public SetDeliveryDayEventHandler(IOrderManager orderManager, IOrderRepository orderRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork]
        public async Task HandleEventAsync(SetDeliveryDayEto eventData)
        {
            var entity = await _orderRepository.FindAsync(eventData.OrderId);

            entity.SetDeliveryDays(eventData.DeliveryDay);
        }
    }
}
