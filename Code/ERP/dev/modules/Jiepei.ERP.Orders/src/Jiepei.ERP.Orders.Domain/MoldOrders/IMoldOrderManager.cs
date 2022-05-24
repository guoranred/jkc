using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public interface IMoldOrderManager : IDomainService
    {
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="isPassed"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> CheckAsync(MoldOrder moldOrder, bool isPassed, string remark);

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="totalMoney"></param>
        /// <param name="sellingMoney"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> OfferAsync(MoldOrder moldOrder, decimal? totalMoney,decimal? sellingMoney, string remark);

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> ManufactureAsync(MoldOrder moldOrder, string remark);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="trackingNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> DeliverAsync(MoldOrder moldOrder, string trackingNo, string courierCompany, string remark);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> CompleteAsync(MoldOrder moldOrder, string remark);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> CancelAsync(MoldOrder moldOrder, string remark);

        /// <summary>
        /// 设计变更
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<MoldOrder> DesignChange(MoldOrder moldOrder, string fileName, string filePath, string picture, decimal? proMoney, string remark);

        #region 

        #endregion
    }
}
