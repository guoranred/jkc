using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Orders
{
    public class ManufactureOrderEventHandler : ILocalEventHandler<ManufactureOrderEto>, ITransientDependency
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public ManufactureOrderEventHandler(IOrderManager orderManager, IOrderRepository orderRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork(true)]
        public async Task HandleEventAsync(ManufactureOrderEto eventData)
        {
            var order = await _orderRepository.GetAsync(eventData.OrderId);
            await _orderManager.ManufactureAsync(order);
        }
    }
}
