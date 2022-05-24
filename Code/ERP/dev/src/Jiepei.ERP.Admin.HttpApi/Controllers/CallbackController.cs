using Jiepei.BizMO.DeliverCenters.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos;
using Jiepei.ERP.Orders.Admin.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Admin.Controllers
{
    [Route("api/callback")]
    public class CallbackController : ERPController
    {
        private readonly IOrderManagementAppService _orderManagementAppService;

        public CallbackController(IOrderManagementAppService orderManagementAppService)
        {
            _orderManagementAppService = orderManagementAppService;
        }

        [HttpPost("notify")]
        public async Task NotifyAsync()
        {
            try
            {
                Request.EnableBuffering();
                Request.Body.Position = 0;
                using (var stream = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var reqStr = await stream.ReadToEndAsync();

                    ValidateBody(reqStr);

                    ValidateHeaders();

                    var proType = Request.Headers.GetOrDefault("ProType").ToString();
                    var action = Request.Headers.GetOrDefault("Action").ToString();



                    await CallbackHandle(proType, action, reqStr);
                }

            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        private void ValidateBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                throw new UserFriendlyException($"{nameof(body)} 不能为空");
            }

            if (!IsSignatureCompatible("81123dfc-13e3-4c03-ab1e-2a0b32052a69", body))//read webhooksecret from user secret
            {
                throw new UserFriendlyException("Unexpected Signature");
            }
        }

        private void ValidateHeaders()
        {
            if (!Request.Headers.ContainsKey("ProType"))
            {
                throw new UserFriendlyException("请求头 ProType 缺失");
            }

            if (!Request.Headers.ContainsKey("Action"))
            {
                throw new UserFriendlyException("请求头 Action 缺失");
            }
        }

        private bool IsSignatureCompatible(string secret, string body)
        {
            if (!Request.Headers.ContainsKey("jp-webhook-signature"))
            {
                return false;
            }


            var receivedSignature = Request
                                    .Headers
                                    .GetOrDefault("jp-webhook-signature")
                                    .ToString()
                                    .Split('=');

            string computedSignature;
            switch (receivedSignature[0])
            {
                case "sha256":
                    var secretBytes = Encoding.UTF8.GetBytes(secret);
                    using (var hasher = new HMACSHA256(secretBytes))
                    {
                        var data = Encoding.UTF8.GetBytes(body);
                        computedSignature = BitConverter.ToString(hasher.ComputeHash(data));
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            return computedSignature == receivedSignature[1];
        }

        private async Task CallbackHandle(string proType, string action, string body)
        {

            //switch (proType)
            //{
            //    case "BJ":
            await HandleAsync(action, body,  proType);
            //        break;
            //    case "CNC":
            //        await
            //    default:
            //        throw new NotImplementedException();
            //}
        }

        private async Task HandleAsync(string action, string body, string orderNoSuffix)
        {
            if(string.IsNullOrEmpty(action))
                throw new UserFriendlyException("找不到事件名 action："+ action);

            var jsons = JsonConvert.DeserializeObject<dynamic>(body);
            var subOrder = await _orderManagementAppService.GetSubOrderByOrderNo((string)jsons.OrderNo);
            switch (action)
            {
                case "Check":// 审核
                    await _orderManagementAppService.CheckAsync(subOrder.OrderNo, new CheckInput
                    {
                        IsPassed = jsons.IsPassed,
                        SupplierId = default,
                        Remark = jsons.Remark
                    });
                    break;
                case "Offer":// 报价
                    await _orderManagementAppService.OfferAsync(subOrder.OrderNo, new OfferInput
                    {
                        Cost = jsons.Cost,
                        SellingPrice = jsons.ActualPayableMoney,
                        ShipPrice = jsons.ShipMoney,
                        DiscountMoney = jsons.PreferentialMoney,
                        Remark = jsons.Remark
                    });
                    break;
                case "Produce":// 投产
                    await _orderManagementAppService.ManufactureAsync(subOrder.OrderNo, new ManufactureInput
                    {
                        Remark = ""
                    });
                    break;
                case "Deliver":// 发货
                    await _orderManagementAppService.DeliverAsync(subOrder.OrderNo, new DeliverDto
                    {
                        TrackingNo = jsons.TrackingNo,
                        CourierCompany = jsons.CourierCompany,
                    });
                    break;
                case "Cancel":// 取消
                    await _orderManagementAppService.CancelAsync(subOrder.OrderNo, new CancelInput { Rremark = jsons.CancelReason });
                    break;
                case "SetDeliveryDays":// 设置交期
                    await _orderManagementAppService.SetDeliveryDaysAsync(subOrder.OrderNo, new SetDeliveryDayDto
                    {
                        DeliveryDays = jsons.DeliveryDays,
                    });
                    break;
                case "setapplicationArea":
                    throw new UserFriendlyException("");
                case "setdeliveryrequirements":
                    throw new UserFriendlyException("");
                case "SetOrderDelivery"://修改用户发货信息
                    await _orderManagementAppService.Orderdelivery(subOrder.OrderNo,
                        new OrderDeliveryDto
                        {
                            CityCode = jsons.CityCode,
                            CityName = jsons.CityName,
                            CountyCode = jsons.CountyCode,
                            CountyName = jsons.CountyName,
                            ProvinceCode = jsons.ProvinceCode,
                            ReceiverCompany = jsons.ReceiverCompany,
                            ProvinceName = jsons.ProvinceName,
                            ReceiverAddress = jsons.ReceiverAddress,
                            ReceiverName = jsons.ReceiverName,
                            ReceiverTel = jsons.ReceiverTel,
                        });
                    break;
                case "ConfirmRecepit":
                    await _orderManagementAppService.CompleteAsync(subOrder.OrderNo );
                    break;
                default:
                    throw new UserFriendlyException("");
            }
        }
    }
}
