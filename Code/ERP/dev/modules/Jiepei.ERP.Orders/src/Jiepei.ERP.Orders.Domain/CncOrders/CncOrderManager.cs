using Jiepei.ERP.Commons;
using Jiepei.ERP.Cncs;
using Jiepei.ERP.Orders.Orders;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using Jiepei.ERP.EventBus.Shared.Cncs;

namespace Jiepei.ERP.Orders.CncOrders
{
    public class CncOrderManager : DomainService, ICncOrderManager
    {
        private readonly ICncOrderRepository _cncOrderRepository;
        private readonly ICncOrderFlowRepository _cncOrderFlowRepository;
        private readonly IOrderDeliveryRepository _orderDeliveryRepository;

        private readonly IOrderManager _orderManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CncOrderManager(ICncOrderRepository cncOrderRepository
          , ICncOrderFlowRepository cncOrderFlowRepository
          , IOrderDeliveryRepository orderDeliveryRepository
          , IOrderManager orderManager
          , IDistributedEventBus distributedEventBus
          , IUnitOfWorkManager unitOfWorkManager)
        {
            _cncOrderRepository = cncOrderRepository;
            _cncOrderFlowRepository = cncOrderFlowRepository;
            _orderDeliveryRepository = orderDeliveryRepository;

            _orderManager = orderManager;
            _distributedEventBus = distributedEventBus;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public virtual async Task<CncOrder> CancelAsync(CncOrder CncOrder, string remark)
        {
            if (CncOrder.Status >= (int)EnumCncOrderStatus.Cancel || CncOrder.Status == (int)EnumCncOrderStatus.CheckedNoPass)
                throw new UserFriendlyException(message: "当前订单状态不符合取消条件");

            var order = await _orderManager.GetOrderByOrderNo(CncOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CancelCncEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.CancelAsync(order);
            await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.Cancel, remark, "取消订单");

            await uow.CompleteAsync();

            return CncOrder;
        }

        public virtual async Task<CncOrder> CheckAsync(CncOrder CncOrder, bool isPassed, string remark)
        {
            if (CncOrder.Status != (int)EnumCncOrderStatus.WaitCheck
                && CncOrder.Status != (int)EnumCncOrderStatus.CheckedNoPass
                && CncOrder.Status != (int)EnumCncOrderStatus.CheckedPass
                )
            {
                throw new UserFriendlyException(message: "当前订单状态不符合审核条件");
            }

            var order = await _orderManager.GetOrderByOrderNo(CncOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
            {
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CheckCncEto
                {
                    OrderNo = order.ExterOrderNo,
                    Remark = remark,
                    Status = isPassed ? EnumCncOrderStatus.CheckedPass : EnumCncOrderStatus.CheckedNoPass
                }));
            }

            await _orderManager.CheckAsync(order, isPassed);

            if (isPassed)
            {
                await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.CheckedPass, remark, "审核通过");
            }
            else
            {
                await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.CheckedNoPass, remark, "审核不通过");
            }

            await uow.CompleteAsync();

            return CncOrder;
        }
        public async Task<CncOrder> CompleteAsync(CncOrder CncOrder, string remark)

        {
            if (CncOrder.Status != (int)EnumCncOrderStatus.HaveSend)
                throw new UserFriendlyException(message: "当前订单状态不符合完成条件");

            var order = await _orderManager.GetOrderByOrderNo(CncOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CompleteCncEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.CompleteAsync(order);
            await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.Finish, remark, "完成订单");

            await uow.CompleteAsync();

            return CncOrder;
        }

        public async Task<CncOrder> DeliverAsync(CncOrder CncOrder, string trackingNo, string courierCompany, string remark)
        {
            if (CncOrder.Status != (int)EnumCncOrderStatus.Production)
                throw new UserFriendlyException(message: "当前订单状态不符合发货条件");

            var order = await _orderManager.GetOrderByOrderNo(CncOrder.MainOrderNo);
            var delivery = await _orderDeliveryRepository.GetAsync(t => t.OrderNo == order.OrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new DeliverCncEto
                {
                    OrderNo = order.ExterOrderNo,
                    CourierCompany = courierCompany,
                    TrackingNo = trackingNo,
                    SendTime = DateTime.Now
                }));

            await _orderManager.DeliverAsync(order);
            await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.HaveSend, remark, "订单发货");

            // TODO: 考虑通过 EventBus 修改发货状态。
            delivery.SetTrackingNo(trackingNo);
            delivery.SetCourierCompany(courierCompany);
            await _orderDeliveryRepository.UpdateAsync(delivery);

            await uow.CompleteAsync();

            return CncOrder;
        }

        public async Task<CncOrder> OfferAsync(CncOrder CncOrder, decimal? totalMoney, decimal? sellingMoney, string remark)
        {
            if (CncOrder.Status != (int)EnumCncOrderStatus.CheckedPass && CncOrder.Status != (int)EnumCncOrderStatus.OfferComplete)
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");

            var order = await _orderManager.GetOrderByOrderNo(CncOrder.MainOrderNo);

            // 付款后不能修改报价
            if (order.IsPay.GetValueOrDefault())
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () =>
                {
                    await _distributedEventBus.PublishAsync(new OfferCncEto
                    {
                        OrderNo = order.ExterOrderNo,
                        SellingMoney = sellingMoney ?? 0
                    });
                });

            await _orderManager.OfferAsync(order, totalMoney, sellingMoney);
            await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.OfferComplete, remark, "报价完成");

            await uow.CompleteAsync();

            return CncOrder;
        }

        public async Task<CncOrder> ManufactureAsync(CncOrder CncOrder, string remark)
        {
            if (CncOrder.Status != (int)EnumCncOrderStatus.SureOrder)
                throw new UserFriendlyException(message: "当前订单状态不符合投产条件");

            var order = await _orderManager.GetOrderByOrderNo(CncOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new ManufactureCncEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.ManufactureAsync(order);
            await ChangeStatusAsync(CncOrder, (int)EnumCncOrderStatus.Production, remark, "订单投产");

            await uow.CompleteAsync();

            return CncOrder;
        }

        private async Task ChangeStatusAsync(CncOrder CncOrder, int status, string remark, string note)
        {
            Check.NotNull(CncOrder, nameof(CncOrder));

            CncOrder.SetStatus(status);

            CncOrder = await _cncOrderRepository.UpdateAsync(CncOrder);

            await CncOrderFlowAsync(CncOrder.OrderNo, status, remark, note);

        }

        private async Task CncOrderFlowAsync(string OrderNo, int status, string remark, string note)
        {
            var CncOrderFlow = new CncOrderFlow(GuidGenerator.Create(), OrderNo, status, remark, note);

            await _cncOrderFlowRepository.InsertAsync(CncOrderFlow);
        }
    }
}