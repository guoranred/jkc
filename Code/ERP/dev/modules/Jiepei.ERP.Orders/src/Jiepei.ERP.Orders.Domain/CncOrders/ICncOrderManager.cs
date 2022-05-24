
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.CncOrders
{
    public interface ICncOrderManager : IDomainService
    {
        /// <summary>
        /// ��˶���
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="isPassed"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> CheckAsync(CncOrder injectionOrder , bool isPassed, string remark);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="totalMoney"></param>
        /// <param name="sellingMoney"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> OfferAsync(CncOrder injectionOrder, decimal? totalMoney, decimal? sellingMoney, string remark);
        /// <summary>
        /// Ͷ��
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> ManufactureAsync(CncOrder injectionOrder, string remark);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="trackingNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> DeliverAsync(CncOrder injectionOrder, string trackingNo, string courierCompany, string remark);
        /// <summary>
        /// ��ɶ���
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> CompleteAsync(CncOrder injectionOrder, string remark);
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<CncOrder> CancelAsync(CncOrder injectionOrder, string remark);
    }
}