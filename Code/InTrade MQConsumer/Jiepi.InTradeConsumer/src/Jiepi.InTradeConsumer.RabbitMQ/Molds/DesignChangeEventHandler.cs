using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Jiepei.InTradeConsumer.MoldOrders;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.OrderDetails;
using Volo.Abp.Uow;
using Jiepei.ERP.Molds;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.ERP.EventBus.Shared.Molds;
using Microsoft.Extensions.Logging;

namespace Jiepi.InTradeConsumer.Service
{
    /// <summary>
    /// 设计变更
    /// </summary>
    public class DesignChangeEventHandler : IDistributedEventHandler<DesignChangeMoldEto>, ITransientDependency
    {
        private readonly IRepository<MoldOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<DesignChangeEventHandler> _logger;

        public DesignChangeEventHandler(IRepository<MoldOrder, int> orderRepository,
            IRepository<OrderDetail, int> orderDetailRepository
            , IRepository<OrderMain, int> orderMainRepository
            , ILogger<DesignChangeEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(DesignChangeMoldEto eventData)
        {
            if (!await ValidAsync(eventData))
            {
                return;
            }

            await DesignChangeMoldOrderAsync(eventData);
            //var mainId = await DesignChangeOrderDetailAsync(eventData);
            //await DesignChangeOrderMainAsync(mainId, eventData);
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(DesignChangeMoldEto eventData)
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

            if (order.Status >= (int)EnumMoldOrderStatus.HaveSend)
            {
                _logger.LogError($"订单{eventData.OrderNo}不符合设计变更");
                return default;
            }
            return true;
        }

        [UnitOfWork]
        protected virtual async Task DesignChangeMoldOrderAsync(DesignChangeMoldEto eventData)
        {
            var entity = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            entity.SetFileName(eventData.FileName);
            entity.SetFilePath(eventData.FilePath);
            entity.SetPicture(eventData.Picture);
            await _orderRepository.UpdateAsync(entity);
        }

        protected virtual async Task<int> DesignChangeOrderDetailAsync(DesignChangeMoldEto eventData)
        {
            var entity = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            entity.SetPayMoney(eventData.SellingMoney ?? 0 - entity.TotalMoney ?? 0 + entity.PayMoney ?? 0);
            entity.SetTotalMoney(eventData.SellingMoney ?? 0);
            await _orderDetailRepository.UpdateAsync(entity);
            return entity?.MainId ?? 0;
        }

        protected virtual async Task DesignChangeOrderMainAsync(int id,DesignChangeMoldEto eventData)
        {
            var entity = await _orderMainRepository.FindAsync(t => t.Id == id);
            entity.SetPayMoney(eventData.SellingMoney ?? 0- entity.TotalMoney ?? 0 + entity.OrderPayMoney??0);
            entity.SetTotalMoney(eventData.SellingMoney??0);
            await _orderMainRepository.UpdateAsync(entity);
        }
    }
}
