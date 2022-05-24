using Jiepei.ERP.EventBus.Shared.Molds;
using Jiepei.ERP.Molds;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.InTradeConsumer.MoldOrders;
using Jiepei.InTradeConsumer.OrderDetails;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Jiepi.InTradeConsumer.Service
{
    /// <summary>
    /// 订单报价
    /// </summary>
    public class OfferEventHandler : IDistributedEventHandler<OfferMoldEto>, ITransientDependency
    {
        private readonly IRepository<MoldOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<OfferEventHandler> _logger;

        public OfferEventHandler(
            IRepository<MoldOrder, int> orderRepository,
            IRepository<OrderDetail, int> orderDetailRepository,
            IRepository<OrderMain, int> orderMainRepository,
            ILogger<OfferEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(OfferMoldEto eventData)
        {
            if (!await ValidAsync(eventData))
            {
                return;
            }

            await ChangeOrderStatusAsync(eventData);
            var mainId = await ChangeOrderDetailStatusAsync(eventData);
            await ChangeOrderMainStatusAsync(eventData, mainId);
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(OfferMoldEto eventData)
        {
            if (eventData == null)
            {
                _logger.LogError("系统异常");
                return default;
            }
            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            if (order == null)
            {
                _logger.LogError($"未查询到当前模具订单{eventData.OrderNo}信息");
                return default;
            }
            if (order.Status > (int)EnumMoldOrderStatus.OfferComplete)
            {
                _logger.LogError($"订单{eventData.OrderNo}不符合报价条件");
                return default;
            }
            return true;
        }


        [UnitOfWork]
        protected virtual async Task ChangeOrderStatusAsync(OfferMoldEto eventData)
        {
            var entity = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            entity.Offer((int)EnumMoldOrderStatus.OfferComplete, eventData.SellingMoney);
            await _orderRepository.UpdateAsync(entity);
        }

        [UnitOfWork]
        protected virtual async Task<int> ChangeOrderDetailStatusAsync(OfferMoldEto eventData)
        {
            var entity = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            entity.SetTotalMoney(eventData.SellingMoney);
            entity.SetStatus((int)EnumOrderDetailStatus.CheckedPass);
            await _orderDetailRepository.UpdateAsync(entity);
            return entity?.MainId ?? 0;
        }

        [UnitOfWork]
        protected virtual async Task ChangeOrderMainStatusAsync(OfferMoldEto eventData, int id)
        {
            var entity = await _orderMainRepository.FindAsync(t => t.Id == id);
            entity.Status = (int)EnumOrderMainStatus.WaitSure;
            entity.SetTotalMoney(eventData.SellingMoney);
            await _orderMainRepository.UpdateAsync(entity);
        }
    }
}
