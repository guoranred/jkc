using Jiepei.ERP.Commons;
using Jiepei.ERP.EventBus.Shared.Molds;
using Jiepei.ERP.Molds;
using Jiepei.ERP.Orders.Orders;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public class MoldOrderManager : DomainService, IMoldOrderManager
    {

        private readonly IMoldOrderRepository _moldOrderRepository;
        private readonly IMoldOrderFlowRepository _moldOrderFlowRepository;
        private readonly IOrderDeliveryRepository _orderDeliveryRepository;

        private readonly IOrderManager _orderManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MoldOrderManager(IMoldOrderRepository moldOrderRepository
            , IMoldOrderFlowRepository moldOrderFlowRepository
            , IOrderDeliveryRepository orderDeliveryRepository
            , IOrderManager orderManager
            , IDistributedEventBus distributedEventBus
            , IUnitOfWorkManager unitOfWorkManager)
        {
            _moldOrderRepository = moldOrderRepository;
            _moldOrderFlowRepository = moldOrderFlowRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderManager = orderManager;
            _distributedEventBus = distributedEventBus;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public virtual async Task<MoldOrder> CancelAsync(MoldOrder moldOrder, string remark)
        {
            if (moldOrder.Status >= (int)EnumMoldOrderStatus.Cancel || moldOrder.Status == (int)EnumMoldOrderStatus.CheckedNoPass)
                throw new UserFriendlyException(message: "当前订单状态不符合取消条件");

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CancelMoldEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.CancelAsync(order);
            await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.Cancel, remark, "取消订单");

            await uow.CompleteAsync();

            return moldOrder;
        }

        public virtual async Task<MoldOrder> CheckAsync(MoldOrder moldOrder, bool isPassed, string remark)
        {
            if (moldOrder.Status != (int)EnumMoldOrderStatus.WaitCheck
                && moldOrder.Status != (int)EnumMoldOrderStatus.CheckedNoPass
                && moldOrder.Status != (int)EnumMoldOrderStatus.CheckedPass
                )
            {
                throw new UserFriendlyException(message: "当前订单状态不符合审核条件");
            }

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
            {
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CheckMoldEto
                {
                    OrderNo = order.ExterOrderNo,
                    Remark = remark,
                    Status = isPassed ? EnumMoldOrderStatus.CheckedPass : EnumMoldOrderStatus.CheckedNoPass
                }));
            }

            await _orderManager.CheckAsync(order, isPassed);

            if (isPassed)
            {
                await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.CheckedPass, remark, "审核通过");
            }
            else
            {
                await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.CheckedNoPass, remark, "审核不通过");
            }

            await uow.CompleteAsync();

            return moldOrder;
        }

        public async Task<MoldOrder> CompleteAsync(MoldOrder moldOrder, string remark)
        {
            if (moldOrder.Status != (int)EnumMoldOrderStatus.HaveSend)
                throw new UserFriendlyException(message: "当前订单状态不符合完成条件");

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new CompleteMoldEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.CompleteAsync(order);
            await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.Finish, remark, "完成订单");

            await uow.CompleteAsync();

            return moldOrder;
        }

        public async Task<MoldOrder> DeliverAsync(MoldOrder moldOrder, string trackingNo, string courierCompany, string remark)
        {
            if (moldOrder.Status != (int)EnumMoldOrderStatus.Purchasing)
                throw new UserFriendlyException(message: "当前订单状态不符合发货条件");

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);
            var delivery = await _orderDeliveryRepository.GetAsync(t => t.OrderNo == order.OrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new DeliverMoldEto
                {
                    OrderNo = order.ExterOrderNo,
                    CourierCompany = courierCompany,
                    TrackingNo = trackingNo,
                    SendTime = DateTime.Now
                }));

            await _orderManager.DeliverAsync(order);
            await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.HaveSend, remark, "订单发货");

            // TODO: 考虑通过 EventBus 修改发货状态。
            delivery.SetTrackingNo(trackingNo);
            delivery.SetCourierCompany(courierCompany);
            await _orderDeliveryRepository.UpdateAsync(delivery);

            await uow.CompleteAsync();

            return moldOrder;
        }

        public async Task<MoldOrder> OfferAsync(MoldOrder moldOrder, decimal? totalMoney, decimal? sellingMoney, string remark)
        {
            if (moldOrder.Status != (int)EnumMoldOrderStatus.CheckedPass && moldOrder.Status != (int)EnumMoldOrderStatus.OfferComplete)
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);

            // 付款后不能修改报价
            if (order.IsPay.GetValueOrDefault())
                throw new UserFriendlyException(message: "当前订单状态不符合报价条件");

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () =>
                {
                    await _distributedEventBus.PublishAsync(new OfferMoldEto
                    {
                        OrderNo = order.ExterOrderNo,
                        SellingMoney = sellingMoney ?? 0
                    });
                });

            await _orderManager.OfferAsync(order, totalMoney, sellingMoney);
            await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.OfferComplete, remark, "报价完成");

            await uow.CompleteAsync();

            return moldOrder;
        }

        public async Task<MoldOrder> ManufactureAsync(MoldOrder moldOrder, string remark)
        {
            if (moldOrder.Status != (int)EnumMoldOrderStatus.SureOrder)
                throw new UserFriendlyException(message: "当前订单状态不符合投产条件");

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new ManufactureMoldEto { OrderNo = order.ExterOrderNo }));

            await _orderManager.ManufactureAsync(order);
            await ChangeStatusAsync(moldOrder, (int)EnumMoldOrderStatus.Purchasing, remark, "订单投产");

            await uow.CompleteAsync();

            return moldOrder;
        }

        public async Task<MoldOrder> DesignChange(MoldOrder moldOrder, string fileName, string filePath, string picture, decimal? proMoney, string remark)
        {
            if (moldOrder.Status >= (int)EnumMoldOrderStatus.Purchasing)
                throw new UserFriendlyException(message: "当前订单状态不符合设计变更条件");

            var order = await _orderManager.GetOrderByOrderNo(moldOrder.MainOrderNo);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.Origin == (int)EnumOrigin.InternalTrade)
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new DesignChangeMoldEto
                {
                    OrderNo = order.ExterOrderNo,
                    FileName = fileName,
                    FilePath = filePath,
                    Picture = picture,
                    SellingMoney = 0,
                    TotalMoney = 0,
                    Remark = remark,
                }));
            await _orderManager.DesignChangeAsync(order, proMoney);
            moldOrder.SetFileName(fileName);
            moldOrder.SetFilePath(filePath);
            moldOrder.SetPicture(picture);
            moldOrder = await _moldOrderRepository.UpdateAsync(moldOrder);
            await MoldOrderFlowAsync(moldOrder.OrderNo, (int)moldOrder.Status, remark, "设计更改");
            return moldOrder;
        }
        private async Task ChangeStatusAsync(MoldOrder moldOrder, int status, string remark, string note)
        {
            Check.NotNull(moldOrder, nameof(MoldOrder));

            moldOrder.SetStatus(status);

            moldOrder = await _moldOrderRepository.UpdateAsync(moldOrder);

            await MoldOrderFlowAsync(moldOrder.OrderNo, status, remark, note);
        }

        private async Task MoldOrderFlowAsync(string OrderNo, int status, string remark, string note)
        {
            var moldOrderFlow = new MoldOrderFlow(GuidGenerator.Create(), OrderNo, status, remark, note);

            await _moldOrderFlowRepository.InsertAsync(moldOrderFlow);
        }
    }
}
