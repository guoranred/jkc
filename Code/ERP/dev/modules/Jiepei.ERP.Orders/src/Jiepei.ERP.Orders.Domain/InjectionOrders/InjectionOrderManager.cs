using Jiepei.ERP.Commons;
using Jiepei.ERP.EventBus.Shared.Injections;
using Jiepei.ERP.Injections;
using Jiepei.ERP.Orders.Orders;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public class InjectionOrderManager : DomainService, IInjectionOrderManager
    {
        private readonly IInjectionOrderRepository _injectionOrderRepository;
        private readonly IInjectionOrderFlowRepository _injectionOrderFlowRepository;
        private readonly IOrderDeliveryRepository _orderDeliveryRepository;

        private readonly IOrderManager _orderManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public InjectionOrderManager(IInjectionOrderRepository injectionOrderRepository
          , IInjectionOrderFlowRepository injectionOrderFlowRepository
          , IOrderDeliveryRepository orderDeliveryRepository
          , IOrderManager orderManager
          , IDistributedEventBus distributedEventBus
          , IUnitOfWorkManager unitOfWorkManager)
        {
            _injectionOrderRepository = injectionOrderRepository;
            _injectionOrderFlowRepository = injectionOrderFlowRepository;
            _orderDeliveryRepository = orderDeliveryRepository;

            _orderManager = orderManager;
            _distributedEventBus = distributedEventBus;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public virtual async Task<InjectionOrder> CancelAsync(InjectionOrder injectionOrder, string remark)
        {
            if (injectionOrder.Status >= (int)EnumInjectionOrderStatus.Cancel || injectionOrder.Status == (int)EnumInjectionOrderStatus.CheckedNoPass)
                throw new UserFriendlyException(message: "当前订单状态不符合取消条件");

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CancelInjectionEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.CancelAsync(order);
            await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.Cancel, remark, "取消订单");

            await uow.CompleteAsync();

            return injectionOrder;
        }

        public virtual async Task<InjectionOrder> CheckAsync(InjectionOrder injectionOrder, bool isPassed, string remark)
        {
            if (injectionOrder.Status != (int)EnumInjectionOrderStatus.WaitCheck
                && injectionOrder.Status != (int)EnumInjectionOrderStatus.CheckedNoPass
                && injectionOrder.Status != (int)EnumInjectionOrderStatus.CheckedPass
                )
            {
                throw new UserFriendlyException(message: "当前订单状态不符合审核条件");
            }

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
            {
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CheckInjectionEto
                {
                    OrderNo = order.ExterOrderNo,
                    Remark = remark,
                    Status = isPassed ? EnumInjectionOrderStatus.CheckedPass : EnumInjectionOrderStatus.CheckedNoPass
                }));
            }

            await _orderManager.CheckAsync(order, isPassed);

            if (isPassed)
            {
                await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.CheckedPass, remark, "审核通过");
            }
            else
            {
                await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.CheckedNoPass, remark, "审核不通过");
            }

            await uow.CompleteAsync();

            return injectionOrder;
        }
        public async Task<InjectionOrder> CompleteAsync(InjectionOrder injectionOrder, string remark)

        {
            if (injectionOrder.Status != (int)EnumInjectionOrderStatus.HaveSend)
                throw new UserFriendlyException(message: "当前订单状态不符合完成条件");

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CompleteInjectionEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.CompleteAsync(order);
            await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.Finish, remark, "完成订单");

            await uow.CompleteAsync();

            return injectionOrder;
        }

        public async Task<InjectionOrder> DeliverAsync(InjectionOrder injectionOrder, string trackingNo, string courierCompany, string remark)
        {
            if (injectionOrder.Status != (int)EnumInjectionOrderStatus.Purchasing)
                throw new UserFriendlyException(message: "当前订单状态不符合发货条件");

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);
            var delivery = await _orderDeliveryRepository.GetAsync(t => t.OrderNo == order.OrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new DeliverInjectionEto
                {
                    OrderNo = order.ExterOrderNo,
                    CourierCompany = courierCompany,
                    TrackingNo = trackingNo,
                    SendTime = DateTime.Now
                }));

            await _orderManager.DeliverAsync(order);
            await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.HaveSend, remark, "订单发货");

            // TODO: 考虑通过 EventBus 修改发货状态。
            delivery.SetTrackingNo(trackingNo);
            delivery.SetCourierCompany(courierCompany);
            await _orderDeliveryRepository.UpdateAsync(delivery);

            await uow.CompleteAsync();

            return injectionOrder;
        }

        public async Task<InjectionOrder> OfferAsync(InjectionOrder injectionOrder, decimal? totalMoney, decimal? sellingMoney, string remark)
        {
            if (injectionOrder.Status != (int)EnumInjectionOrderStatus.CheckedPass && injectionOrder.Status != (int)EnumInjectionOrderStatus.OfferComplete)
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);

            // 付款后不能修改报价
            if (order.IsPay.GetValueOrDefault())
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () =>
                {
                    await _distributedEventBus.PublishAsync(new OfferInjectionEto
                    {
                        OrderNo = order.ExterOrderNo,
                        SellingMoney = sellingMoney ?? 0
                    });
                });

            await _orderManager.OfferAsync(order, totalMoney, sellingMoney);
            await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.OfferComplete, remark, "报价完成");

            await uow.CompleteAsync();

            return injectionOrder;
        }

        public async Task<InjectionOrder> ManufactureAsync(InjectionOrder injectionOrder, string remark)
        {
            if (injectionOrder.Status != (int)EnumInjectionOrderStatus.SureOrder)
                throw new UserFriendlyException(message: "当前订单状态不符合投产条件");

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new ManufactureInjectionEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.ManufactureAsync(order);
            await ChangeStatusAsync(injectionOrder, (int)EnumInjectionOrderStatus.Purchasing, remark, "订单投产");

            await uow.CompleteAsync();

            return injectionOrder;
        }

        public async Task<InjectionOrder> DeliveryDaysAsync(InjectionOrder injectionOrder, int deliveryDays, string remark) {

            if (injectionOrder.Status >= (int)EnumInjectionOrderStatus.Purchasing || injectionOrder.Status == (int)EnumInjectionOrderStatus.Cancel)
                throw new UserFriendlyException(message: "当前订单状态不符合修改交期条件");

            var order = await _orderManager.GetOrderByOrderNo(injectionOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new DeliveryDaysInjectionEto { OrderNo = order.ExterOrderNo ,DeliveryDays= deliveryDays }));

            order.SetDeliveryDays(deliveryDays);
            await ChangeStatusAsync(injectionOrder, (int)injectionOrder.Status, remark, "修改交期");

            await uow.CompleteAsync();

            return injectionOrder;

        }
        private async Task ChangeStatusAsync(InjectionOrder injectionOrder, int status, string remark, string note)
        {
            Check.NotNull(injectionOrder, nameof(InjectionOrder));

            injectionOrder.SetStatus(status);

            injectionOrder = await _injectionOrderRepository.UpdateAsync(injectionOrder);

            await InjectionOrderFlowAsync(injectionOrder.OrderNo, status, remark, note);

        }

        private async Task InjectionOrderFlowAsync(string OrderNo, int status, string remark, string note)
        {
            var injectionOrderFlow = new InjectionOrderFlow(GuidGenerator.Create(), OrderNo, status, remark, note);

            await _injectionOrderFlowRepository.InsertAsync(injectionOrderFlow);
        }
    }
}