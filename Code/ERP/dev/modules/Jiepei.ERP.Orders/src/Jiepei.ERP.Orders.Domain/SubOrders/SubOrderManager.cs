using Jiepei.ERP.EventBus.Shared.SubOrders;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.SubOrders;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderManager : DomainService, ISubOrderManager
    {
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IOrderManager _orderManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<SubOrderFlow> _subOrderFlow;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ISubOrderMoldItemRepository _subOrderMoldItemRepository;
        private readonly ISubOrderInjectionItemRepository _subOrderInjectionItemRepository;
        private readonly ISubOrderCncItemRepository _subOrderCncItemRepository;
        private readonly ISubOrderThreeDItemRepository _subOrderThreeDItemRepository;

        public SubOrderManager(ISubOrderRepository subOrderRepository,
                               IOrderManager orderManager,
                               IUnitOfWorkManager unitOfWorkManager,
                               IRepository<SubOrderFlow> subOrderFlow,
                               IDistributedEventBus distributedEventBus,
                               ISubOrderMoldItemRepository subOrderMoldItemRepository,
                               ISubOrderInjectionItemRepository subOrderInjectionItemRepository,
                               ISubOrderCncItemRepository subOrderCncItemRepository,
                               ISubOrderThreeDItemRepository subOrderThreeDItemRepository)
        {
            _subOrderRepository = subOrderRepository;
            _orderManager = orderManager;
            _unitOfWorkManager = unitOfWorkManager;
            _subOrderFlow = subOrderFlow;
            _distributedEventBus = distributedEventBus;
            _subOrderMoldItemRepository = subOrderMoldItemRepository;
            _subOrderInjectionItemRepository = subOrderInjectionItemRepository;
            _subOrderCncItemRepository = subOrderCncItemRepository;
            _subOrderThreeDItemRepository = subOrderThreeDItemRepository;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public virtual async Task<SubOrder> CancelAsync(Guid id, string remark)
        {

            var subOrder = await _subOrderRepository.GetAsync(t => t.Id == id);
            //if (subOrder.Status >= EnumSubOrderStatus.Cancel || subOrder.Status == EnumSubOrderStatus.CheckedNoPass)
            //    throw new UserFriendlyException(message: "当前订单状态不符合取消条件");

            var order = await _orderManager.GetOrderByOrderNoAsync(subOrder.OrderId);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            await _orderManager.CancelAsync(order);


            await ChangeStatusAsync(subOrder, EnumSubOrderStatus.Cancel, remark, "取消订单");

            await uow.CompleteAsync();

            return subOrder;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public virtual async Task<SubOrder> CancelAsync(SubOrder subOrder, string remark)
        {

            subOrder.Cancel(remark);

            await _subOrderRepository.UpdateAsync(subOrder);

            await CreateSubOrderFlowAsync(subOrder.OrderNo, EnumSubOrderFlowType.Cancel, remark, "取消订单");

            return subOrder;
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trackingNo"></param>
        /// <param name="courierCompany"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<SubOrder> DeliverAsync(Guid id, string trackingNo, string courierCompany, string remark)
        {
            var subOrder = await _subOrderRepository.GetAsync(t => t.Id == id);
            if (subOrder.OrderType == EnumOrderType.Print3D)
            {
                if (subOrder.Status != EnumSubOrderStatus.Inbound)
                    throw new UserFriendlyException(message: "当前订单状态不符合发货条件");
            }
            else
            {
                if (subOrder.Status != EnumSubOrderStatus.Purchasing)
                    throw new UserFriendlyException(message: "当前订单状态不符合发货条件");
            }

            var order = await _orderManager.GetOrderByOrderNoAsync(subOrder.OrderId);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            await _orderManager.DeliverAsync(order);

            order.TrackingNo = trackingNo;
            order.CourierCompany = courierCompany;

            subOrder.OutboundTime = Clock.Now;
            await _subOrderRepository.UpdateAsync(subOrder);

            if (order.ChannelId == Guid.NewGuid())
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new SubOrderDeliverEto
                {
                    OrderNo = order.ChannelOrderNo,
                    CourierCompany = courierCompany,
                    TrackingNo = trackingNo,
                    SendTime = DateTime.Now,
                    OrderType = order.OrderType,
                }));

            await ChangeStatusAsync(subOrder, EnumSubOrderStatus.HaveSend, remark, "订单发货");

            await uow.CompleteAsync();

            return subOrder;
        }


        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="trackingNo">运单号</param>
        /// <param name="courierCompany">快递公司</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public async Task DeliverAsync(SubOrder subOrder, string trackingNo, string courierCompany, string remark)
        {
            subOrder.Deliver(trackingNo, courierCompany);

            await CreateSubOrderFlowAsync(subOrder.OrderNo, EnumSubOrderFlowType.Deliver, remark, "订单发货");

            await _subOrderRepository.UpdateAsync(subOrder);
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public async Task<SubOrder> CompleteAsync(Guid id, string remark)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                throw new UserFriendlyException("订单不存在");

            var uow = _unitOfWorkManager.Begin(isTransactional: true);

            entity.Complete();

           // await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo, EnumSubOrderFlowType.Complete, "完成订单", remark);

            await uow.CompleteAsync();

            return entity;
        }

        public async Task CompleteAsync(string orderNo)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.OrderNo == orderNo);

            entity.Complete();

            await CreateSubOrderFlowAsync(entity.OrderNo, EnumSubOrderFlowType.Complete, "完成订单", "");

            await _subOrderRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="isPassed">是否通过</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public async Task CheckAsync(Guid id, bool isPassed, string remark)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                throw new UserFriendlyException("订单不存在");

            var uow = _unitOfWorkManager.Begin(isTransactional: true);

            entity.Check(isPassed, remark);

            await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo
                , isPassed ? EnumSubOrderFlowType.Check : EnumSubOrderFlowType.CheckNoPass
                , $"审核订单=>{(isPassed ? "审核通过" : "审核不通过")}"
                , remark);

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="entity"><see cref="SubOrder"/></param>
        /// <param name="isPassed">是否通过</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task CheckAsync(SubOrder entity, bool isPassed, string remark)
        {
            var uow = _unitOfWorkManager.Begin(isTransactional: true);

            entity.Check(isPassed, remark);

            await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo
                , isPassed ? EnumSubOrderFlowType.Check : EnumSubOrderFlowType.CheckNoPass
                , $"审核订单=>{(isPassed ? "审核通过" : "审核不通过")}"
                , remark);

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="cost">成本</param>
        /// <param name="sellingPrice">销售价</param>
        /// <param name="remark">备注</param>
        /// <param name="discountMoney">优惠金额</param>
        /// <param name="shipPrice">运费</param>
        /// <returns></returns>
        public async Task OfferAsync(Guid id, decimal cost, decimal sellingPrice, decimal shipPrice, decimal discountMoney, string remark)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                throw new UserFriendlyException("订单不存在");

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            var resultPrice = 0m;

            if (entity.OrderType == EnumOrderType.SheetMetal)
                resultPrice = sellingPrice + shipPrice - discountMoney;
            else
                resultPrice = sellingPrice * 1.08m + shipPrice - discountMoney;

            if (resultPrice <= 0)
                throw new UserFriendlyException("总报价金额不能小于0");

            entity.Offer(cost, sellingPrice, shipPrice, discountMoney);

            await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo, EnumSubOrderFlowType.Offer, $"订单报价", remark);

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="entity"><see cref="SubOrder"/></param>
        /// <param name="cost">成本</param>
        /// <param name="sellingPrice">销售价</param>
        /// <param name="remark">备注</param>
        /// <param name="discountMoney">优惠金额</param>
        /// <param name="shipPrice">运费</param>
        /// <returns></returns>
        public async Task OfferAsync(SubOrder entity, decimal cost, decimal sellingPrice, decimal shipPrice, decimal discountMoney, string remark)
        {
            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            var resultPrice = 0m;

         //   if (entity.OrderType == EnumOrderType.SheetMetal)
                resultPrice = sellingPrice + shipPrice - discountMoney;
            //else
            //    resultPrice = sellingPrice * 1.08m + shipPrice - discountMoney;
            if (resultPrice <= 0)
                throw new UserFriendlyException("总报价金额不能小于0");

            entity.Offer(cost, sellingPrice, shipPrice, discountMoney);

            await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo, EnumSubOrderFlowType.Offer, $"订单报价", remark);

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public async Task ManufactureAsync(Guid id, string remark)
        {
            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            var entity = await GetByIdAsync(id);

            entity.Manufacture();

            await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo, EnumSubOrderFlowType.Offer, $"投产", remark);

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="entity"><see cref="SubOrder"/></param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public async Task ManufactureAsync(SubOrder entity, string remark)
        {
            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            entity.Manufacture();

            await _subOrderRepository.UpdateAsync(entity);

            await CreateSubOrderFlowAsync(entity.OrderNo, EnumSubOrderFlowType.Offer, $"投产", remark);

            await uow.CompleteAsync();
        }

        /// <summary>
        /// 设计变更
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="picture"></param>
        /// <param name="proMoney"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<SubOrder> DesignChange(Guid id, string fileName, string filePath, string picture, decimal proMoney, string remark)
        {
            var subOrder = await GetByIdAsync(id);
            if (subOrder.OrderType != EnumOrderType.Mold)
                throw new UserFriendlyException(message: "当前订单类型不为模具");

            if (subOrder.Status >= EnumSubOrderStatus.Purchasing)
                throw new UserFriendlyException(message: "当前订单状态不符合设计变更条件");

            var order = await _orderManager.GetOrderByOrderNoAsync(subOrder.OrderId);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.ChannelId == Guid.NewGuid())
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new SubOrderDesignChangeEto
                {
                    OrderNo = order.ChannelOrderNo,
                    FileName = fileName,
                    FilePath = filePath,
                    Picture = picture,
                    SellingMoney = 0,
                    TotalMoney = 0,
                    Remark = remark,
                    OrderType = order.OrderType,
                }));
            await _orderManager.DesignChangeAsync(order, proMoney);

            var moldOrder = await _subOrderMoldItemRepository.GetAsync(t => t.SubOrderId == subOrder.Id);
            moldOrder.SetFileName(fileName);
            moldOrder.SetFilePath(filePath);
            moldOrder.SetPicture(picture);
            moldOrder = await _subOrderMoldItemRepository.UpdateAsync(moldOrder);

            await CreateSubOrderFlowAsync(subOrder.OrderNo, EnumSubOrderFlowType.DesignChange, remark, "设计更改");
            return subOrder;
        }
        /// <summary>
        /// 修改交期
        /// </summary>
        /// <param name="subOrderId"></param>
        /// <param name="deliveryDays"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<SubOrder> UpdateDeliveryDaysAsync(Guid subOrderId, int deliveryDays, string remark)
        {

            var subOrder = await _subOrderRepository.GetAsync(t => t.Id == subOrderId);

            if (subOrder.OrderType != EnumOrderType.Injection)
                throw new UserFriendlyException(message: "当前订单类型不为注塑");

            if (subOrder.Status >= EnumSubOrderStatus.Purchasing || subOrder.Status == EnumSubOrderStatus.Cancel)
                throw new UserFriendlyException(message: "当前订单状态不符合修改交期条件");

            var order = await _orderManager.GetOrderByOrderNoAsync(subOrder.OrderId);

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);
            if (order.ChannelId == Guid.NewGuid())
                uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new SubOrderDeliveryDaysEto
                {
                    OrderNo = order.ChannelOrderNo,
                    DeliveryDays = deliveryDays,
                    OrderType = order.OrderType,
                }));

            order.SetDeliveryDays(deliveryDays);
            await CreateSubOrderFlowAsync(subOrder.OrderNo, EnumSubOrderFlowType.ChangeDeliveryDays, remark, "修改交期");

            await uow.CompleteAsync();

            return subOrder;
        }

        /// <summary>
        /// 设置交期
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="deliveryDays"></param>
        /// <returns></returns>
        public async Task SetDeliveryDaysAsync(SubOrder subOrder, int deliveryDays)
        {
            subOrder.SetDeliveryDays(deliveryDays);

            await CreateSubOrderFlowAsync(subOrder.OrderNo, EnumSubOrderFlowType.ChangeDeliveryDays, "", "修改交期");
        }

        public async Task<SubOrder> GetByIdAsync(Guid id)
        {
            return await _subOrderRepository.GetAsync(t => t.Id == id);
        }

        private async Task ChangeStatusAsync(SubOrder subOrder, EnumSubOrderStatus status, string remark, string note)
        {
            Check.NotNull(subOrder, nameof(SubOrder));

            subOrder.SetStatus(status);

            subOrder = await _subOrderRepository.UpdateAsync(subOrder);

            await CreateSubOrderFlowAsync(subOrder.OrderNo, EnumSubOrderFlowType.Cancel, remark, note);
        }
        private async Task CreateSubOrderFlowAsync(string OrderNo, EnumSubOrderFlowType status, string content, string remark)
        {
            var subOrderFlow = new SubOrderFlow(GuidGenerator.Create(), OrderNo, status, content, remark);

            await _subOrderFlow.InsertAsync(subOrderFlow);

        }

        /// <summary>
        /// 获取子订单详情
        /// </summary>
        /// <typeparam name="T">子订单扩展信息</typeparam>
        /// <param name="id">主订单 Id</param>
        /// <returns></returns>
        public async Task<Tuple<SubOrder, SubOrderFlow>> GetSubOrderDetail(Guid id)
        {
            var subOrder = await _subOrderRepository.GetAsync(t => t.OrderId == id);
            var subOrderFlow = await _subOrderFlow.GetAsync(t => t.OrderNo == subOrder.OrderNo);
            return new Tuple<SubOrder, SubOrderFlow>(subOrder, subOrderFlow);
        }

        public async Task<SubOrderMoldItem> GetSubOrderMoldItemAsync(Guid id)
        {
            return await _subOrderMoldItemRepository.GetAsync(t => t.SubOrderId == id);
        }

        public async Task<SubOrderInjectionItem> GetSubOrderInjectionItemAsync(Guid id)
        {
            return await _subOrderInjectionItemRepository.GetAsync(t => t.SubOrderId == id);
        }

        public async Task<SubOrderCncItem> GetSubOrderCncItemAsync(Guid id)
        {
            return await _subOrderCncItemRepository.GetAsync(t => t.SubOrderId == id);
        }
    }
}
