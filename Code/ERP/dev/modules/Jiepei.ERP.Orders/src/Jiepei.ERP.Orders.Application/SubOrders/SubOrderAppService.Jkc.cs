using Jiepei.ERP.Orders.Application.External.Order.Models;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jiepei.ERP.Orders.SubOrders
{
    /// <summary>
    /// 甲壳虫
    /// </summary>
    [Obsolete]
    public partial class SubOrderAppService : OrdersAppService
    {
        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderGroup"></param>
        /// <param name="ProcessParameters"></param>
        /// <returns></returns>
        private async Task SyncOrderInfoAsync(OrderInfoDto order, OrderGroupDto orderGroup, string ProcessParameters)
        {
            //创建第三方同步订单信息所需要的参数
            // var orderInfo = await _OrderInfoRep.GetAsync(id);
            var externalOrderInfo = order;
            var externalOrderGroupInfo = orderGroup;

            var ChannelCode = "JKC";
            externalOrderInfo.OrderChannel = ChannelCode;
            externalOrderInfo.OrderChannelName = "甲壳虫";
            externalOrderInfo.DeliveryDate = DateTime.Now.AddDays(5);
            externalOrderGroupInfo.OrderChannel = ChannelCode;
            externalOrderGroupInfo.DeliveryDate = null;
            externalOrderGroupInfo.CreateTime = DateTime.Now;
            externalOrderGroupInfo.TaxPoint = 0.08m;

            List<OrderBomMaterialDto> bomMaterialList = new List<OrderBomMaterialDto>();
            if (!string.IsNullOrWhiteSpace(ProcessParameters))
            {
                var materialString = ProcessParameters;
                var materialJson = JObject.Parse(materialString);
                var materials = materialJson["bomList"]?.Children();
                if (materials.HasValue && materials.Value.Any())
                {
                    foreach (var material in materials.Value)
                    {
                        var bomMaterialItem = JsonConvert.DeserializeObject<OrderBomMaterialDto>(material.ToString());
                        if (null != bomMaterialItem)
                        {
                            var crafts = material.SelectToken("bomCraftList")?.Children();
                            List<OrderBomCraftDto> bomCraftList = new List<OrderBomCraftDto>();
                            if (crafts.HasValue && crafts.Value.Any())
                            {
                                foreach (var craft in crafts.Value)
                                {
                                    var bomCraftItem = JsonConvert.DeserializeObject<OrderBomCraftDto>(craft.ToString());
                                    bomCraftList.Add(bomCraftItem);
                                }
                            }
                            bomMaterialItem.OrderBomCraftDTOs = bomCraftList;
                        }
                        bomMaterialList.Add(bomMaterialItem);
                    }
                }
            }
            await _orderExternalService.SyncOrderInfoAsync(new CreateOrderDto(externalOrderInfo, bomMaterialList));
            await _orderExternalService.SyncOrderGroupAsync(externalOrderGroupInfo);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <returns></returns> 
        private async Task UpdateStatusAsync(string orderNo, CancelInput input)
        {
            await _orderExternalService.CancelOrderInfoAsync(new CancelOrderDto(orderNo, input.Rremark, input.Rremark) { });
        }


        /// <summary>
        /// 获取在线计价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> CreateProductPriceAsync(ProductPriceInput input)
        {
            var productPriceInputDto = ObjectMapper.Map<ProductPriceInput, ProductPriceInputDto>(input);
            var res = await _orderExternalService.GetProductPriceAsync(productPriceInputDto);
            return JsonConvert.SerializeObject(res);
        }

        /// <summary>
        /// 获取在线计价初始化参数
        /// </summary>
       [AllowAnonymous]
        public async Task<string> GetMaterialInitListForFrontAsync()
        {
            var res = await _orderExternalService.GetMaterialInitListForFrontAsync();
            return JsonConvert.SerializeObject(res);
        }


        /// <summary>
        /// 修改订单(重新上传文件)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<bool> UpdateAsync(UpdateOrderProductFileDto input)
        {
            await _orderExternalService.ModifyOrderProductFileAsync(input);
            return true;
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        private async Task<bool> FinishOrder(string orderNo)
        {
            await _orderExternalService.OrderFinishAsync(orderNo);
            return true;
        }
    }
}
