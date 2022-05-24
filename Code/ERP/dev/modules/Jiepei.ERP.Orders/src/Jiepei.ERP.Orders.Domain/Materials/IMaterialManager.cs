using Jiepei.ERP.Orders.Materals;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.Materials
{
    public interface IMaterialManager : IDomainService
    {
        /// <summary>
        /// 计价
        /// </summary>
        /// <param name="channelId">渠道id</param>
        /// <param name="materialId">材料id</param>
        /// <param name="HandleMethod">后处理方式(表面处理)</param>
        /// <param name="num">数量</param>
        /// <param name="volume">密度</param>
        /// <param name="handleMethodDesc">后处理描述</param>
        /// <returns></returns>
        Task<MateralValuationEto> Calculation3DPrice(Guid channelId, Guid materialId, string handleMethod, int num, decimal volume, string handleMethodDesc);

        /// <summary>
        /// 计算交期天数
        /// </summary>
        /// <param name="channelId">渠道id</param>
        /// <param name="materialId">材料id</param>
        /// <param name="HandleMethod">后处理方式(表面处理)</param>
        /// <param name="handleMethodDesc">后处理描述</param>
        /// <returns></returns>
        Task<int> Calculation3DDelivery(Guid channelId, Guid materialId, string handleMethod, string handleMethodDesc);

        /// <summary>
        /// 计算交期日期
        /// </summary>
        /// <param name="n">交期天数</param>
        /// <returns></returns>
        Task<DateTime> Calculation3DDeliveryDays(int n);

        /// <summary>
        /// 后处理费计算
        /// </summary>
        /// <param name="HandleMethod">后处理方式(表面处理)</param>
        /// <param name="handleMethodDesc">后处理描述</param>
        /// <returns></returns>
        int GetHandleMethodDescPrice(string handleMethod, string handleMethodDesc);

        /// <summary>
        /// 单价计算
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="density"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        decimal GetUnitPrice(decimal volume, decimal density, decimal price, decimal unitStartPrice);
        /// <summary>
        /// 重量
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="density"></param>
        /// <returns></returns>
        decimal GetWeight(decimal volume, decimal density, int count);
    }
}
