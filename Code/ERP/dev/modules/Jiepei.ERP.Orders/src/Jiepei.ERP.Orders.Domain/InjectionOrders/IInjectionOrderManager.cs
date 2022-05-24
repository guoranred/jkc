using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public interface IInjectionOrderManager : IDomainService
    {
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="isPassed"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> CheckAsync(InjectionOrder injectionOrder , bool isPassed, string remark);
        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="totalMoney"></param>
        /// <param name="sellingMoney"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> OfferAsync(InjectionOrder injectionOrder, decimal? totalMoney, decimal? sellingMoney, string remark);
        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> ManufactureAsync(InjectionOrder injectionOrder, string remark);
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="trackingNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> DeliverAsync(InjectionOrder injectionOrder, string trackingNo, string courierCompany, string remark);
        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> CompleteAsync(InjectionOrder injectionOrder, string remark);
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> CancelAsync(InjectionOrder injectionOrder, string remark);
        /// <summary>
        /// 修改交期
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> DeliveryDaysAsync(InjectionOrder injectionOrder, int deliveryDays, string remark);
    }
}