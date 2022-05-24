using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Orders
{
    public class CheckOrderEventHandler : ILocalEventHandler<CheckOrderEto>, ITransientDependency
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public CheckOrderEventHandler(IOrderManager orderManager, IOrderRepository orderRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork(true)]
        public async Task HandleEventAsync(CheckOrderEto eventData)
        {
            var order = await _orderRepository.GetAsync(eventData.OrderId);
            await _orderManager.CheckAsync(order, eventData.IsPassed);
        }
    }
}
