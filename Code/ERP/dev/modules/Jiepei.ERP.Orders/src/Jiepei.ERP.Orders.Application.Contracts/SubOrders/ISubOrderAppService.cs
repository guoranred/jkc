using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderAppService : IApplicationService
    {
        /// <summary>
        /// 创建订单（3D）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderBaseDto> CreateThreeDAsync(CreateOrderExtraDto input);

        /// <summary>
        /// 创建订单（钣金）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderBaseDto> CreateSheetMetalAsync(CreateOrderExtraDto input);

        /// <summary>
        /// 创建订单（CNC）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderBaseDto> CreateCncAsync(CreateOrderExtraDto input);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CancelAsync(Guid id, CancelInput input);

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CompleteAsync(Guid id, CompleteInput input);

        /// <summary>
        /// 材料获取
        /// </summary>
        /// <returns></returns>
        Task<string> GetMaterialInitListForFrontAsync();

        /// <summary>
        /// 计价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> CreateProductPriceAsync(ProductPriceInput input);

        /// <summary>
        /// 修改订单(重新上传文件)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderFileAsync(UpdateOrderInput input);
        Task PaymentAsync(OrderDto order);
    }
}
