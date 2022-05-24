using Jiepei.ERP.EventBus.Shared.Cncs;
using Jiepei.ERP.Cncs;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.InTradeConsumer.OrderDetails;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using Jiepei.InTradeConsumer.Domain.CncOrders;

namespace Jiepi.InTradeConsumer.Service.Cncs
{
    /// <summary>
    /// 订单投产
    /// </summary>
    public class IntroductionEventHandler : IDistributedEventHandler<ManufactureCncEto>, ITransientDependency
    {
        private readonly IRepository<CncOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<IntroductionEventHandler> _logger;

        public IntroductionEventHandler(IRepository<CncOrder, int> orderRepository
            , IRepository<OrderDetail, int> orderDetailRepository
            , IRepository<OrderMain, int> orderMainRepository
            , ILogger<IntroductionEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(ManufactureCncEto eventData)
        {
            if (!await ValidAsync(eventData))
            {
                return;
            }

            await ChangeOrderStatusAsync(eventData);
            var mainId = await ChangeOrderDetailStatusAsync(eventData);
            await ChangeOrderMainStatusAsync(mainId);
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(ManufactureCncEto eventData)
        {         
            if (eventData == null)
            {
                _logger.LogError("系统异常");
                return default;
            }

            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            if (order == null)
            {
                _logger.LogError($"未查询到当前CNC订单{eventData.OrderNo}信息");
                return default;
            }

            if (order.Status >= (int)EnumCncOrderStatus.Production)
            {
                _logger.LogError($"订单{eventData.OrderNo}不符合投产条件");
                return default;
            }
            return true;
        }


        [UnitOfWork]
        protected virtual async Task ChangeOrderStatusAsync(ManufactureCncEto eventData)
        {
            var entity = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            entity.SetStaus((int)EnumCncOrderStatus.Production);
            await _orderRepository.UpdateAsync(entity);
        }

        [UnitOfWork]
        protected virtual async Task<int> ChangeOrderDetailStatusAsync(ManufactureCncEto eventData)
        {
            var entity = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            entity.SetStatus((int)EnumOrderDetailStatus.Purchasing);
            entity.SetSureConfirmTime(DateTime.Now);
            await _orderDetailRepository.UpdateAsync(entity);
            return entity?.MainId ?? 0;
        }

        protected virtual async Task ChangeOrderMainStatusAsync(int id)
        {
            var entity = await _orderMainRepository.FindAsync(t => t.Id == id);
            entity.SetStatus((int)EnumOrderMainStatus.SureOrder);
            await _orderMainRepository.UpdateAsync(entity);
        }
    }
}
