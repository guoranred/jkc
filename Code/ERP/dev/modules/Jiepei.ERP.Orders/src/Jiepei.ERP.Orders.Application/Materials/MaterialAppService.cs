using Jiepei.ERP.Members.MemberAddresses;
using Jiepei.ERP.Orders.Materials.Dtos;
using Jiepei.ERP.Utilities.Ship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.Orders.Materials
{
    [Authorize]
    public class MaterialAppService : OrdersAppService, IMaterialAppService
    {
        private readonly ID3MaterialRepository _d3MaterialRepository;
        private readonly IMaterialPriceRepository _materialPriceRepository;
        private readonly IMaterialManager _materialManager;
        private readonly IHttpClientFactory _clientFactory;
        private readonly NeiMao _neiMaoOptions;
        private readonly IMemberAddressAppService _memberAddressAppService;

        public MaterialAppService(ID3MaterialRepository d3MaterialRepository
            , IMaterialPriceRepository materialPriceRepository
            , IMaterialManager materialManager
            , IHttpClientFactory clientFactory
            , IOptions<NeiMao> neiMaoOptions
            , IMemberAddressAppService memberAddressAppService
            )
        {
            _d3MaterialRepository = d3MaterialRepository;
            _materialPriceRepository = materialPriceRepository;
            _materialManager = materialManager;
            _clientFactory = clientFactory;
            _neiMaoOptions = neiMaoOptions.Value;
            _memberAddressAppService = memberAddressAppService;
        }

        #region 材料渠道管理
        /// <summary>
        /// 按渠道获取3d材料列表
        /// </summary>
        /// <param name="channelMaterialListDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<ChannelMaterialsDto>> GetChannelMaterialListAsync(ChannelMaterialListDto channelMaterialListDto)
        {
            var d3MaterialEntiy = await _d3MaterialRepository.GetQueryableAsync();
            var materialPriceEntiy = await _materialPriceRepository.GetQueryableAsync();
            var query = materialPriceEntiy
                .Where(t => t.OrderType == channelMaterialListDto.OrderType)
                .Where(t => t.ChannelId == channelMaterialListDto.ChannelId)
                .Where(t => t.IsSale == true)
                .Join(d3MaterialEntiy, o => o.MaterialId, e => e.Id, (o, e) => new
                   ChannelMaterialsDto
                {
                    MaterialId = o.MaterialId,
                    ChannelId = o.ChannelId,
                    Price = o.Price,
                    OrderType = o.OrderType,
                    Code = e.Code,
                    PartCode = e.PartCode,
                    Name = e.Name,
                    Category = e.Category,
                    Density = e.Density,
                    Attr = e.Attr,
                    Excellence = e.Excellence,
                    Short = e.Short,
                    Color = e.Color,
                    MinSinWeight = e.MinSinWeight,
                    Note = e.Note
                });
            var entiy = await AsyncExecuter.ToListAsync(query);
            return entiy;
        }

        ///// <summary>
        ///// 甲壳虫计价材料获取
        ///// </summary>
        ///// <returns></returns>
        //public async Task<string> GetMaterialInitListForFrontAsync()
        //{
        //    //var res = await _orderExternalService.GetMaterialInitListForFrontAsync();
        //    //return JsonConvert.SerializeObject(res);
        //    return null;
        //    //var res = await _subOrderAppService.GetMaterialInitListForFrontAsync();
        //    //return JsonConvert.SerializeObject(res);
        //}
        #endregion

        #region 计价相关
        ///// <summary>
        ///// 钣金计价
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        // public async Task<string> CreateProductPriceAsync(ProductPriceInput input)
        // {
        //     var productPriceInputDto = ObjectMapper.Map<ProductPriceInput, ProductPriceInputDto>(input);
        //     //var res = await _orderExternalService.GetProductPriceAsync(productPriceInputDto);
        //     //return JsonConvert.SerializeObject(res);
        //     return null;
        //     //var res = await _subOrderAppService.CreateProductPriceAsync(input);
        //     //return JsonConvert.SerializeObject(res);
        // }
        /// <summary>
        /// 3d计价
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<CalculationResultDto>> Calculation3DPrice(List<Calculation3DPriceDto> calculation3DPriceDto)
        {

            var calculationResultDto = new List<CalculationResultDto>();

            foreach (var entiy in calculation3DPriceDto ?? new List<Calculation3DPriceDto>())
            {
                // var handleMethodDesc = JsonConvert.SerializeObject(entiy.HandleMethodDesc);

                //计价
                var materalValuation = await _materialManager.Calculation3DPrice(
                     entiy.ChannelId
                   , entiy.MaterialId
                   , entiy.HandleMethod
                   , entiy.Num
                   , entiy.Volume
                   , entiy.HandleMethodDesc);
                //交期天数
                var deliveryNum = await _materialManager.Calculation3DDelivery(
                     entiy.ChannelId
                   , entiy.MaterialId
                   , entiy.HandleMethod
                   , entiy.HandleMethodDesc);
                //交期日期
                var deliveryDate = await _materialManager.Calculation3DDeliveryDays(deliveryNum);

                calculationResultDto.Add(new CalculationResultDto
                {
                    MaterialId = entiy.MaterialId,
                    Price = materalValuation.Price,
                    Delivery = deliveryNum,
                    DeliveryDate = deliveryDate,
                    unitPrice = materalValuation.UnitPrice
                }); ; ;
            }

            return calculationResultDto;
        }
        public async Task<decimal> GetShipMoney(decimal weight, string code)
        {

            var RelationNmId = await _memberAddressAppService.GetRelationNmId(code);
            return await GetShipMoney(weight / 1000, RelationNmId, "SF", 8);
        }

        public async Task<decimal> GetShipMoney(decimal weight, int provinceId, string shipType, int grouptype)
        {
            var client = _clientFactory.CreateClient();
            var url = _neiMaoOptions.ShipUrl;
            var appsecret = _neiMaoOptions.Appsecret;

            var time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;


            var neiMaoShipParameterDto = new NeiMaoShipParameterDto(
               shipUrl: url,
               appsecret: appsecret,
               timestamp: time,
               shipTpe: shipType,
               weight: weight,
               tjWeight: 0,
               provinceName: "",
               provinceId: provinceId,
               grouptype: grouptype,
               promoney: 0,
               mbId: 0
                );

            var json = JsonConvert.SerializeObject(neiMaoShipParameterDto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var s = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NeiMaoResult>(s);
                if (result.Code == 1)
                    return result.Data;
                return 0m;
            }
            return 0m;
        }
        #endregion
    }
}
