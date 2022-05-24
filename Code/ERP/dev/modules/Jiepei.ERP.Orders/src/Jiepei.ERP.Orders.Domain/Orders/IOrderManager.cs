using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.Orders
{
    public interface IOrderManager : IDomainService
    {
        Task<Order> CheckAsync(Order order, bool isPassed);
        Task<Order> OfferAsync(Order order, decimal cost, decimal sellingPrice, decimal shipPrice, decimal discountMoney);
        Task<Order> ManufactureAsync(Order order);
        Task<Order> DeliverAsync(Order order);
        Task<Order> CompleteAsync(Order order);
        Task<Order> CancelAsync(Order order);
        Task<Order> GetOrderByOrderNoAsync(Guid OrderId);
        Task<Order> DesignChangeAsync(Order orderNo, decimal proMoney);
        Task<Order> GetByIdAsync(Guid id);
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="order">订单<see cref="Order"/></param>
        /// <param name="trackingNo">运单号</param>
        /// <param name="courierCompany">快递公司</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Task<Order> DeliverAsync(Order order, string trackingNo, string courierCompany);
    }
}
