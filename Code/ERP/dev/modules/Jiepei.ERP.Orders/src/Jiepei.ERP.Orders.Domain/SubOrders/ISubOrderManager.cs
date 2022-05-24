using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderManager : IDomainService
    {
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<SubOrder> CancelAsync(Guid subOrderId, string remark);

        [Obsolete]
        Task CheckAsync(Guid id, bool isPassed, string remark);

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cost"></param>
        /// <param name="sellingPrice"></param>
        /// <param name="shipPrice"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [Obsolete]
        Task OfferAsync(Guid id, decimal cost, decimal sellingPrice, decimal shipPrice, decimal DiscountMoney, string remark);

        [Obsolete]
        Task ManufactureAsync(Guid id, string remark);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="trackingNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<SubOrder> DeliverAsync(Guid subOrderId, string trackingNo, string courierCompany, string remark);

        Task<SubOrder> CompleteAsync(Guid id, string remark);

        /// <summary>
        /// 设计变更
        /// </summary>
        /// <param name="moldOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<SubOrder> DesignChange(Guid subOrderId, string fileName, string filePath, string picture, decimal proMoney, string remark);


        /// <summary>
        /// 修改交期
        /// </summary>
        /// <param name="InjectionOrder"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<SubOrder> UpdateDeliveryDaysAsync(Guid subOrderId, int deliveryDays, string remark);

        Task<SubOrder> GetByIdAsync(Guid id);

        Task<Tuple<SubOrder, SubOrderFlow>> GetSubOrderDetail(Guid id);
        Task<SubOrderMoldItem> GetSubOrderMoldItemAsync(Guid id);
        Task<SubOrderInjectionItem> GetSubOrderInjectionItemAsync(Guid id);
        Task<SubOrderCncItem> GetSubOrderCncItemAsync(Guid id);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="entity"><see cref="SubOrder"/></param>
        /// <param name="isPassed">是否通过</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Task CheckAsync(SubOrder entity, bool isPassed, string remark);

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
        Task OfferAsync(SubOrder entity, decimal cost, decimal sellingPrice, decimal shipPrice, decimal discountMoney, string remark);

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="entity"><see cref="SubOrder"/></param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Task ManufactureAsync(SubOrder entity, string remark);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="trackingNo">运单号</param>
        /// <param name="courierCompany">快递公司</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Task DeliverAsync(SubOrder subOrder, string trackingNo, string courierCompany, string remark);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        Task<SubOrder> CancelAsync(SubOrder subOrder, string remark);

        /// <summary>
        /// 设置交期
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="deliveryDays"></param>
        /// <returns></returns>
        Task SetDeliveryDaysAsync(SubOrder subOrder, int deliveryDays);
        Task CompleteAsync(string orderNo);
    }
}
