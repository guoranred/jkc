using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public interface IInjectionOrderManager : IDomainService
    {
        /// <summary>
        /// ��˶���
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="isPassed"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> CheckAsync(InjectionOrder injectionOrder , bool isPassed, string remark);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="totalMoney"></param>
        /// <param name="sellingMoney"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> OfferAsync(InjectionOrder injectionOrder, decimal? totalMoney, decimal? sellingMoney, string remark);
        /// <summary>
        /// Ͷ��
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> ManufactureAsync(InjectionOrder injectionOrder, string remark);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="trackingNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> DeliverAsync(InjectionOrder injectionOrder, string trackingNo, string courierCompany, string remark);
        /// <summary>
        /// ��ɶ���
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> CompleteAsync(InjectionOrder injectionOrder, string remark);
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> CancelAsync(InjectionOrder injectionOrder, string remark);
        /// <summary>
        /// �޸Ľ���
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<InjectionOrder> DeliveryDaysAsync(InjectionOrder injectionOrder, int deliveryDays, string remark);
    }
}