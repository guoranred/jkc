using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Admin
{
    public class ThreeDPrintLogisticsAppService : OrdersAdminAppServiceBase, IThreeDPrintLogisticsAppService
    {
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISubOrderThreeDItemRepository _orderThreeDItemRepository;
        private readonly IRepository<OrderDelivery, Guid> _orderDeliveryRepository;
        private readonly ISubOrderManager _subOrderManager;

        public ThreeDPrintLogisticsAppService(ISubOrderRepository subOrderRepository,
                                              IOrderRepository orderRepository,
                                              ISubOrderThreeDItemRepository orderThreeDItemRepository,
                                              IRepository<OrderDelivery, Guid> orderDeliveryRepository,
                                              ISubOrderManager subOrderManager)
        {
            _subOrderRepository = subOrderRepository;
            _orderRepository = orderRepository;
            _orderThreeDItemRepository = orderThreeDItemRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _subOrderManager = subOrderManager;
        }

        public async Task<Dictionary<string, long>> GetThreeDPrintLogisticsCountByStausAsync()
        {
            var subOrders = await _subOrderRepository.GetQueryableAsync();
            var query = subOrders.Where(t => t.OrderType == EnumOrderType.Print3D);
            var allTotal = await AsyncExecuter.LongCountAsync(query.Where(t =>
            t.Status == EnumSubOrderStatus.Inbound || t.Status == EnumSubOrderStatus.HaveSend));
            var inboundTotal = await AsyncExecuter.LongCountAsync(query.Where(t => t.Status == EnumSubOrderStatus.Inbound));
            var outboundTotal = await AsyncExecuter.LongCountAsync(query.Where(t => t.Status == EnumSubOrderStatus.HaveSend));
            return new Dictionary<string, long>
            {
                { nameof(allTotal), allTotal },
                { nameof(inboundTotal), inboundTotal },
                { nameof(outboundTotal), outboundTotal }
            };
        }

        /// <summary>
        /// 3D 物流信息列表 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ThreeDPrintLogisticsDto>> GetThreeDPrintLogisticsListAsync(GetThreeDPrintLogisticsInput input)
        {
            var orders = await _orderRepository.GetQueryableAsync();
            var subOrders = await _subOrderRepository.GetQueryableAsync();

            var query = orders
                .Join(subOrders, order => order.Id, subOrder => subOrder.OrderId,
                (order, subOrder) => new OrderAndSubOrder { Order = order, SubOrder = subOrder });

            query = CreateFilteredQuery(query, input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);

            query = query.PageBy(input);

            var result = await AsyncExecuter.ToListAsync(query);
            var list = await BuildThreeDPrintLogisticsDtos(result);
            return new PagedResultDto<ThreeDPrintLogisticsDto>(totalCount, list);
        }

        public async Task<IEnumerable<SubOrderThreeDItemDto>> GetThreeDPrintBomList(string orderNo)
        {
            Check.NotNullOrEmpty(orderNo, nameof(orderNo));
            var d3OrderExtras = await _orderThreeDItemRepository.GetQueryableAsync();
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo.Trim());
            if (subOrder == null)
                throw new UserFriendlyException($"订单 {orderNo} 不存在");
            var boms = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == subOrder.Id);

            return ObjectMapper.Map<List<SubOrderThreeDItem>, List<SubOrderThreeDItemDto>>(boms);
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        public async Task ChangeInboundNumAsync(List<ChangeInboundNumInput> inputs)
        {
            var entities = await _orderThreeDItemRepository.GetListAsync(t => inputs.Select(input => input.Id).Contains(t.Id));
            var subOrder = await _subOrderRepository.FindAsync(t => t.Id == entities.FirstOrDefault().SubOrderId);

            if (subOrder.Status < EnumSubOrderStatus.Purchasing && subOrder.Status >= EnumSubOrderStatus.HaveSend)
                throw new UserFriendlyException(message: "当前订单状态不符合入库条件");

            inputs.ForEach(input =>
            {
                entities.ForEach(t =>
                {
                    if (t.Id == input.Id)
                    {
                        if (input.InboundNum > t.Count)
                        {
                            throw new UserFriendlyException("入库数量不能大于下单数量");
                        }
                        t.InboundNum = input.InboundNum;
                    }
                });
            });

            subOrder.SetStatus(EnumSubOrderStatus.Inbound);
            subOrder.InboundTime = Clock.Now;

            await _subOrderRepository.UpdateAsync(subOrder);
            await _orderThreeDItemRepository.UpdateManyAsync(entities);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeliverAsync(Guid id, DeliverDto input)
        {
            var entities = await _orderThreeDItemRepository.GetListAsync(t => input.Outbound.Select(o => o.Id).Contains(t.Id));

            input.Outbound.ForEach(o =>
            {
                entities.ForEach(t =>
                {
                    if (t.Id == o.Id)
                    {
                        if (o.OutboundNum > t.Count)
                        {
                            throw new UserFriendlyException("发货数量不能大于下单数量");
                        }
                        t.OutboundNum = o.OutboundNum;
                    }
                });
            });

            await _orderThreeDItemRepository.UpdateManyAsync(entities);

            await _subOrderManager.DeliverAsync(id, input.TrackingNo, input.CourierCompany, input.Remark);
        }

        private async Task<List<ThreeDPrintLogisticsDto>> BuildThreeDPrintLogisticsDtos(List<OrderAndSubOrder> inputs)
        {
            var result = new List<ThreeDPrintLogisticsDto>();
            var deliveries = await GetDeliveries(inputs.Select(t => t.Order.OrderNo).ToArray());
            var threeDItems = await GetThreeDItems(inputs.Select(t => t.SubOrder.Id).ToArray());
            foreach (var item in inputs)
            {
                var delivery = deliveries.Find(t => t.OrderNo == item.Order.OrderNo);
                var supplierOrderCode = GetSupplierOrderCode(threeDItems, item);

                result.Add(new ThreeDPrintLogisticsDto
                {
                    Id = item.SubOrder.Id,
                    OrderNo = item.SubOrder.OrderNo,
                    SupplierOrderCode = supplierOrderCode.Remove(supplierOrderCode.Length - 1),
                    OutboundTime = item.SubOrder.OutboundTime,
                    InboundTime = item.SubOrder.InboundTime,
                    Status = item.SubOrder.Status.GetDescription()
                });
            }

            return result;
        }
        private string GetSupplierOrderCode(List<SubOrderThreeDItem> threeDItems, OrderAndSubOrder input)
        {
            var supplierOrderCode = string.Empty;
            threeDItems
                .Where(t => t.SubOrderId == input.SubOrder.Id)
                .Select(t => t.SupplierOrderCode)
                .Distinct()
                .ToList()
                .ForEach(t => supplierOrderCode += t + ",");
            return supplierOrderCode;
        }

        private async Task<List<OrderDelivery>> GetDeliveries(string[] orderNos)
        {
            return await _orderDeliveryRepository.GetListAsync(t => orderNos.Contains(t.OrderNo));
        }

        private async Task<List<SubOrderThreeDItem>> GetThreeDItems(Guid[] ids)
        {
            return await _orderThreeDItemRepository.GetListAsync(t => ids.Contains(t.SubOrderId));
        }

        private IQueryable<OrderAndSubOrder> CreateFilteredQuery(IQueryable<OrderAndSubOrder> query, GetThreeDPrintLogisticsInput input)
        {
            return query
                .WhereIf(!input.OrderNo.IsNullOrWhiteSpace(), t => t.SubOrder.OrderNo.Contains(input.OrderNo.Trim()))
                .WhereIf(!input.TrackingNo.IsNullOrWhiteSpace(), t => t.Order.TrackingNo.Contains(input.TrackingNo.Trim()))
                .WhereIf(input.Status.HasValue, t => t.SubOrder.Status == input.Status)
                .Where(t => t.SubOrder.OrderType == EnumOrderType.Print3D)
                .Where(t => t.SubOrder.Status >= EnumSubOrderStatus.Inbound && t.SubOrder.Status <= EnumSubOrderStatus.HaveSend);
        }

        private IQueryable<OrderAndSubOrder> ApplySorting(IQueryable<OrderAndSubOrder> query, GetThreeDPrintLogisticsInput input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderByDescending(t => t.SubOrder.CreationTime);
            }
            return query;
        }
        private class OrderAndSubOrder
        {
            public SubOrder SubOrder { get; set; }
            public Order Order { get; set; }
        }
    }
}
