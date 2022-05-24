using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderManager : DomainService, IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<OrderCost> _orderCostRepository;

        public OrderManager(IOrderRepository orderRepository
            , IRepository<OrderLog> orderLogRepository
            , ICurrentUser currentUser
            , IRepository<OrderCost> orderCostRepository)
        {

            _orderRepository = orderRepository;
            _orderLogRepository = orderLogRepository;
            _currentUser = currentUser;
            _orderCostRepository = orderCostRepository;
        }

        public virtual async Task<Order> GetOrderByOrderNoAsync(Guid OrderId)
        {
            return await _orderRepository.GetAsync(t => t.Id == OrderId);
        }

        public virtual async Task<Order> CheckAsync(Order order, bool isPassed)
        {
            if (isPassed)
            {
                return await ChangeStatusAsync(
                    order
                    , EnumOrderStatus.WaitCheck
                    , $"{_currentUser.UserName}:审核通过"
                    );
            }
            else
            {
                return await ChangeStatusAsync(order
                    , EnumOrderStatus.CheckedNoPass
                    , $"{_currentUser.UserName}:审核未通过");
            }
        }

        public virtual async Task<Order> OfferAsync(Order order, decimal cost, decimal proPrice, decimal shipPrice, decimal discountMoney)
        {
            //税费
            var taxMoney = 0m;
            if (order.OrderType != EnumOrderType.SheetMetal&& order.OrderType != EnumOrderType.Cnc)
                taxMoney = proPrice * 0.08m;
            //销售价
            var sellingPrice = proPrice + taxMoney + shipPrice - discountMoney;

            order.Offer(cost, sellingPrice);
            order.SetPendingMoney(sellingPrice);

            var orderCost = await _orderCostRepository.FindAsync(t => t.OrderNo == order.OrderNo);
            orderCost.SetProMoney(proPrice);
            orderCost.SetShipMoney(shipPrice);
            orderCost.SetTaxMoney(taxMoney);
            orderCost.SetDiscountMoney(discountMoney);
            await _orderCostRepository.UpdateAsync(orderCost);

            return await ChangeStatusAsync(order, EnumOrderStatus.CheckedPass, "报价");
        }

        public virtual async Task<Order> ManufactureAsync(Order order)
        {
            return await ChangeStatusAsync(order, EnumOrderStatus.Purchasing, "订单投产");
        }

        public virtual async Task<Order> DeliverAsync(Order order)
        {
            return await ChangeStatusAsync(order, EnumOrderStatus.HaveSend, "订单发货");
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="order"><see cref="Order"/></param>
        /// <param name="trackingNo">运单号</param>
        /// <param name="courierCompany">快递公司</param>
        /// <returns></returns>
        /// <param name="remark"></param>
        public virtual async Task<Order> DeliverAsync(Order order, string trackingNo, string courierCompany)
        {
            order.SetTrackingNo(trackingNo);
            order.SetCourierCompany(courierCompany);
            return await ChangeStatusAsync(order, EnumOrderStatus.HaveSend, "订单发货");
        }


        public virtual async Task<Order> CompleteAsync(Order order)
        {
            return await ChangeStatusAsync(order, EnumOrderStatus.Finish, "完成订单");
        }

        public virtual async Task<Order> CancelAsync(Order order)
        {
            return await ChangeStatusAsync(order, EnumOrderStatus.Cancel, "取消订单");
        }

        private async Task<Order> ChangeStatusAsync(Order order, EnumOrderStatus status, string note)
        {
            Check.NotNull(order, nameof(Order));

            order.SetOrderStatus(status);

            //await _orderRepository.UpdateAsync(order);

            await OrderLogAsync(order, note);

            return order;
        }
        private async Task<OrderLog> OrderLogAsync(Order order, string note)
        {
            var orderLog = new OrderLog(GuidGenerator.Create(), order.OrderNo, note);

            return await _orderLogRepository.InsertAsync(orderLog);
        }
        public async Task<Order> DesignChangeAsync(Order order, decimal proMoney)
        {

            var orderCost = await _orderCostRepository.FindAsync(n => n.OrderNo == order.OrderNo);

            orderCost.SetProMoney(proMoney);

            //order.SetTotalMoney(totalMoney ?? 0);
            //order.SetSellingMoney(sellingMoney ?? 0);
            //order.SetPaidMoney(sellingMoney- (order.PendingMoney??0)??0);

            await OrderLogAsync(order, "设计变更 ,金额:" + proMoney);
            return order;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetAsync(t => t.Id == id);
        }
    }
}
