using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Orders
{
    public class OfferOrderEventHandler : ILocalEventHandler<OfferOrderEto>, ITransientDependency
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public OfferOrderEventHandler(IOrderManager orderManager, IOrderRepository orderRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork(true)]
        public async Task HandleEventAsync(OfferOrderEto eventData)
        {
            var order = await _orderRepository.GetAsync(eventData.OrderId);
            await _orderManager.OfferAsync(order, eventData.Cost, eventData.SellingPrice, eventData.ShipPrice, eventData.DiscountMoney);
        }
    }

}
