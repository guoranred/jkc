using Jiepei.ERP.Orders.Materials.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Materials
{
    public interface IMaterialAppService : IApplicationService
    {
        /// <summary>
        /// 按渠道获取3d材料列表
        /// </summary>
        /// <param name="channelMaterialListDto"></param>
        /// <returns></returns>
        Task<List<ChannelMaterialsDto>> GetChannelMaterialListAsync(ChannelMaterialListDto channelMaterialListDto);
        /// <summary>
        /// 甲壳虫材料
        /// </summary>
        /// <returns></returns>
        //Task<string> GetMaterialInitListForFrontAsync();
        /// <summary>
        /// 钣金计价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<string> CreateProductPriceAsync(ProductPriceInput input);
        /// <summary>
        /// 3d计价
        /// </summary>
        /// <returns></returns>
        Task<List<CalculationResultDto>> Calculation3DPrice(List<Calculation3DPriceDto> calculation3DPriceDto);

        /// <summary>
        /// 运费
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="provinceId"></param>
        /// <param name="shipTpe"></param>
        /// <param name="grouptype"></param>
        /// <returns></returns>
        Task<decimal> GetShipMoney(decimal weight, int provinceId, string shipTpe, int grouptype);

        Task<decimal> GetShipMoney(decimal weight, string provinceName);
    }
}
