using Jiepei.ERP.Orders.Materials.Dtos;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Orders.Materials
{
    [RemoteService(Name = OrderRemoteServiceConsts.RemoteServiceName)]
    [Route("api/materials")]
    public class MaterialController : OrdersController
    {
        private readonly IMaterialAppService _materialAppService;
        private readonly ISubOrderAppService _subOrderAppService;

        public MaterialController(IMaterialAppService materialAppService
            , ISubOrderAppService subOrderAppService)
        {
            _materialAppService = materialAppService;
            _subOrderAppService = subOrderAppService;
        }

        /// <summary>
        /// 按渠道获取3d材料列表
        /// </summary>
        /// <param name="channelMaterialListDto"></param>
        /// <returns></returns>
        [HttpGet("channelmateriallist")]
        public async Task<List<ChannelMaterialsDto>> GetChannelMaterialListAsync(ChannelMaterialListDto channelMaterialListDto)
        {
            return await _materialAppService.GetChannelMaterialListAsync(channelMaterialListDto);
        }
        /// <summary>
        /// 获取钣金材料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("channelmateriallist-sheetMetal")]
        public async Task<string> GetMaterialInitListForFrontAsync()
        {
            return await _subOrderAppService.GetMaterialInitListForFrontAsync();
        }

        /// <summary>
        /// 3d计价
        /// </summary>
        /// <returns></returns>
        [HttpPost("calculation-3dmaterial")]
        public async Task<List<CalculationResultDto>> Calculation3DPrice(List<Calculation3DPriceDto> calculation3DPriceDto)
        {
            return await _materialAppService.Calculation3DPrice(calculation3DPriceDto);
        }
        /// <summary>
        /// 钣金计价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("calculation-sheetMetal")]
        public async Task<string> CreateProductPriceAsync(ProductPriceInput input)
        {
            var res = await _subOrderAppService.CreateProductPriceAsync(input);
            return JsonConvert.SerializeObject(res);
        }

    }
}
