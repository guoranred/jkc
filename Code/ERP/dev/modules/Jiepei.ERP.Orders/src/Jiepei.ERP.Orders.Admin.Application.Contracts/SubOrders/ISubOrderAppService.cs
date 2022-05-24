using Jiepei.ERP.Orders.SubOrders.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderAppService : IApplicationService
    {


        /// <summary>
        /// 修改交期
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeliveryDays(Guid id, DeliveryDaysInput input);

        ///// <summary>
        ///// 设计变更
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task DesignChangeAsync(Guid id, DesignChangeInput input);

        //Task<SubOrderDetailDto> GetDetailAsync(Guid id, EnumOrderType orderType);
    }
}
