using System.Threading.Tasks;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.Orders.Orders
{
    public class CancelOrderEventHandler : ILocalEventHandler<CancelOrderEto>, ITransientDependency
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public CancelOrderEventHandler(IOrderManager orderManager, IOrderRepository orderRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork]
        public async Task HandleEventAsync(CancelOrderEto eventData)
        {
            var entity = await _orderRepository.FindAsync(eventData.OrderId);

            await _orderManager.CancelAsync(entity);
        }
    }
}
