
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.CncOrders
{
    public interface ICncOrderManager : IDomainService
    {
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="isPassed"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> CheckAsync(CncOrder injectionOrder , bool isPassed, string remark);
        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="totalMoney"></param>
        /// <param name="sellingMoney"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> OfferAsync(CncOrder injectionOrder, decimal? totalMoney, decimal? sellingMoney, string remark);
        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> ManufactureAsync(CncOrder injectionOrder, string remark);
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="trackingNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> DeliverAsync(CncOrder injectionOrder, string trackingNo, string courierCompany, string remark);
        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> CompleteAsync(CncOrder injectionOrder, string remark);
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> CancelAsync(CncOrder injectionOrder, string remark);
    }
}