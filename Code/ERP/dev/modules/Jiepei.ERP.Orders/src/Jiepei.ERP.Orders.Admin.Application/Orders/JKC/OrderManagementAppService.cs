using Jiepei.ERP.Orders.Admin.Application.Contracts.SubOrders.Dtos;
using Jiepei.ERP.Orders.Admin.Orders;
using Jiepei.ERP.Orders.Application.External;
using Jiepei.ERP.Orders.Application.External.Order;
using Jiepei.ERP.Orders.Application.External.Order.Models;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.Shared.Enums.SheetMetals;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.Orders.Admin
{
    public partial class OrderManagementAppService : OrdersAdminAppServiceBase, IOrderManagementAppService
    {
        public OrderExternalService _orderExternalService { get; set; }



        /// <summary>
        /// 同步订单至订单中心
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public async Task<ApiHttpResponseDto> SyncOrderInfoAsync(Guid id)
        {
            //创建第三方同步订单信息所需要的参数
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);
            var subOrderFile = await GetOrderTypeFile(subOrder);

            var externalOrderInfo = CreateOrderInfoDtoAsync(order, subOrder, subOrderFile);

            // var externalOrderInfo = ObjectMapper.Map<OrderInfo, OrderInfoDto>(orderInfo);
            List<OrderBomMaterialDto> bomMaterialList = new List<OrderBomMaterialDto>();
            if (!string.IsNullOrWhiteSpace(subOrderFile.ProcessParameters))
            {
                var materialString = subOrderFile.ProcessParameters;
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

            externalOrderInfo.OrderChannel = "JKC";
            externalOrderInfo.OrderChannelName = "甲壳虫";
            var syncOrder = _orderExternalService.SyncOrderInfoAsync(
                new CreateOrderDto(externalOrderInfo, bomMaterialList));

            //创建第三方同步订单包所需要的参数
            var orderGroup = CreateOrderGroupDtoAsync(order);

            orderGroup.OrderChannel = "JKC";
            orderGroup.TaxPoint = 0.08m;
            var syncOrderGroup = _orderExternalService.SyncOrderGroupAsync(orderGroup);

            var result = Task.WhenAll(syncOrder, syncOrderGroup).ContinueWith((response) =>
            {
                var error = response.Result.Where(o => o.Status == false);
                var apiHttpResponses = error as ApiHttpResponse[] ?? error.ToArray();
                if (apiHttpResponses.Any())
                    return apiHttpResponses.First();
                return
                    new ApiHttpResponse(true, "同步订单成功");
            });

            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(result.Result);
            return apiHttpResponse;
        }

        /// <summary>
        /// 根据订单编号从订单中心获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiHttpResponseDto> GetOrderInfoByNoAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            var orderExternal = await _orderExternalService.GetOrderInfoByNoAsync(order.OrderNo);
            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
            return apiHttpResponse;
        }

        /// <summary>
        ///  根据订单编号从订单中心获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<object> GetOrderInfoDetailByNoAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            var orderExternal = await _orderExternalService.GetOrderInfoDetailByNoAsync(order.OrderNo);
            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
            return apiHttpResponse;
        }

        /// <summary>
        /// 修改订单执行状态   确认下单之后,应该是正常执行还是暂停执行(暂停执行则订单生产会暂停)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiHttpResponseDto> ModifyOrderExecutionStatusAsync(Guid id, UpdateOrderStopInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);

            var updateOrderStopExecDto = ObjectMapper.Map<UpdateOrderStopInput, UpdateOrderStopExecDto>(input);
            updateOrderStopExecDto.OrderNo = subOrder.OrderNo;

            var orderExternal = await _orderExternalService.ModifyOrderExecutionStatusAsync(updateOrderStopExecDto);
            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
            return apiHttpResponse;
        }

        /// <summary>
        /// 订单支付确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiHttpResponseDto> ModifyOrderPayStatusAsync(Guid id, UpdateOrderPayStatusInput input)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);

            var updateOrderPayStatusDto = ObjectMapper.Map<UpdateOrderPayStatusInput, UpdateOrderPayStatusDto>(input);
            updateOrderPayStatusDto.GroupNo = order.OrderNo;
            var orderExternal = await _orderExternalService.ModifyOrderPayStatusAsync(updateOrderPayStatusDto);
            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
            return apiHttpResponse;
        }

        /// <summary>
        /// 修改订单文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiHttpResponseDto> ModifyOrderProductFileAsync(Guid id, UpdateOrderProductFileInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);
            var updateOrderProductFileDto = ObjectMapper.Map<UpdateOrderProductFileInput, UpdateOrderProductFileDto>(input);
            updateOrderProductFileDto.OrderNo = subOrder.OrderNo;

            var orderExternal = await _orderExternalService.ModifyOrderProductFileAsync(updateOrderProductFileDto);
            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
            return apiHttpResponse;
        }

        /// <summary>
        /// 取消订单中心的订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiHttpResponseDto> CancelOrderInfoAsync(Guid id, CancelSheetInput input)
        {

            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);
            var cancelOrderDto = ObjectMapper.Map<CancelSheetInput, CancelOrderDto>(input);
            cancelOrderDto.OrderNo = subOrder.OrderNo;

            var orderExternal = await _orderExternalService.CancelOrderInfoAsync(cancelOrderDto);
            var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
            return apiHttpResponse;
        }


        /// <summary>
        /// 第三方调用服务分发
        /// </summary>
        /// <param name="apiSheetMetalDispatcher"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ApiHttpResponseDto> SheetMetalDispatcher(ApiSheetMetalDispatcherInput apiSheetMetalDispatcher)
        {
            DateTime requestTime = DateTime.Now;
            var desKey = "YsB1rrLRzORjQH6l";
            var sign = Convert.ToBase64String(MD5.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(apiSheetMetalDispatcher.Method + apiSheetMetalDispatcher.Parameter + desKey)));
            ApiHttpResponseDto apiResult;
            //MethodBaseInfo methodBaseInfo = new MethodBaseInfo();
            var sw = new Stopwatch();
            var paramters = "";
            //sw.Start();
            if (sign != apiSheetMetalDispatcher.Sign)
            {
                apiResult = new ApiHttpResponseDto("签名对比验证失败,无法解析参数,请与第三方服务提供者联系！");
            }
            else
            {
                var methodName = apiSheetMetalDispatcher.Method;
                paramters = SecurityEncDecrypt.DecryptDes(apiSheetMetalDispatcher.Parameter, desKey);
                switch (methodName)
                {
                    case EnumApiSheetMetalMethod.UpdateOrderMainPrice:
                        var updateOrderGroupPrice = await _sheetMetalOrderGroupService.UpdateOrderGroupPrice(paramters);
                        apiResult = updateOrderGroupPrice.Item1;
                        //methodBaseInfo = updateOrderGroupPrice.Item2;
                        break;
                    case EnumApiSheetMetalMethod.UpdateOrderMainReceiver:
                        var updateOrderMainReceiver = await _sheetMetalOrderGroupService.UpdateOrderMainReceiver(paramters);
                        apiResult = updateOrderMainReceiver.Item1;
                        //methodBaseInfo = updateOrderMainReceiver.Item2;
                        break;
                    case EnumApiSheetMetalMethod.UpdateOrderDetailDeliveryDay:
                        var updateOrderDetailDeliveryDay = await _sheetMetalOrderGroupService.UpdateOrderDetailDeliveryDay(paramters);
                        apiResult = updateOrderDetailDeliveryDay.Item1;
                        //methodBaseInfo = updateOrderDetailDeliveryDay.Item2;
                        break;
                    case EnumApiSheetMetalMethod.DeliverProducts:
                        var deliverProducts = await _sheetMetalOrderGroupService.DeliverProducts(paramters);
                        apiResult = deliverProducts.Item1;
                        //methodBaseInfo = deliverProducts.Item2;
                        break;
                    case EnumApiSheetMetalMethod.UpdateOrderDetailStatus:
                        var updateOrderDetailStatus = await _sheetMetalOrderGroupService.UpdateOrderDetailStatus(paramters);
                        apiResult = updateOrderDetailStatus.Item1;
                        //methodBaseInfo = updateOrderDetailStatus.Item2;
                        break;
                    case EnumApiSheetMetalMethod.UpdateOrderDetailTotalMoney:
                        var updateOrderDetailTotalMoney = await _sheetMetalOrderGroupService.UpdateOrderDetailTotalMoney(paramters);
                        apiResult = updateOrderDetailTotalMoney.Item1;
                        //methodBaseInfo = updateOrderDetailTotalMoney.Item2;
                        break;
                    case EnumApiSheetMetalMethod.UpdateOrderBasic:
                        var updateOrderProductNum = await _sheetMetalOrderGroupService.UpdateOrderProductNum(paramters);
                        apiResult = updateOrderProductNum.Item1;
                        //methodBaseInfo = updateOrderProductNum.Item2;
                        break;
                    default:
                        apiResult = new ApiHttpResponseDto("未找到相应执行的方法，请与管理员联系！");
                        break;
                }
            }
            //sw.Stop();
            //var costTimes = sw.ElapsedMilliseconds;
            //var log = new DispatcherInvokeLog(GuidGenerator.Create(),
            //    methodBaseInfo.NameSpace,
            //    methodBaseInfo.Method,
            //    requestTime,
            //    paramters,
            //    costTimes.ToString(),
            //    JsonConvert.SerializeObject(apiResult));
            //if (!apiResult.Status)
            //    log.SetError(apiResult.Message);
            //await _dispatcherInvokeLogRepository.InsertAsync(log);
            return apiResult;
        }


        private OrderInfoDto CreateOrderInfoDtoAsync(Order order, SubOrder subOrder, SubOrderFile subOrderFile)
        {
            var orderInfoDto = new OrderInfoDto();
            orderInfoDto.OrderNo = subOrder.OrderNo;
            //orderInfoDto.OrderChannel = "JKC";
            orderInfoDto.MemberId = 0;
            orderInfoDto.GroupNo = order.OrderNo;
            //orderInfoDto.OrderChannelName = "甲壳虫";
            //orderInfoDto.DeliveryDate = DateTime.Now.AddDays(5);
            orderInfoDto.FollowAdminName = "";
            orderInfoDto.FrontCalcPrice = 0;
            orderInfoDto.MemberName = CurrentUser.Name;
            orderInfoDto.OrderName = "";
            ///  orderInfoDto.OrderType=
            orderInfoDto.ProductAssembleType = false;
            orderInfoDto.ProductFileName = subOrderFile.FileName;
            orderInfoDto.ProductFilePath = subOrderFile.FilePath;
            //orderInfoDto.ProductFittingFilePath
            orderInfoDto.ProductFittingSourceTypeStr = subOrderFile.PurchasedParts;
            orderInfoDto.ProductNeedDesign = subOrderFile.NeedDesign;
            orderInfoDto.ProductNum = 1;
            orderInfoDto.ProductRemark = order.CustomerRemark;
            orderInfoDto.ProductType = order.OrderType.GetDescription();

            return orderInfoDto;
        }
        private OrderGroupDto CreateOrderGroupDtoAsync(Order order)
        {
            var orderGroupDto = new OrderGroupDto();
            orderGroupDto.GroupNo = order.OrderNo;
            return orderGroupDto;
        }

        private async Task<SubOrderFile> GetOrderTypeFile(SubOrder subOrder)
        {
            switch (subOrder.OrderType)
            {
                case EnumOrderType.Cnc:
                    var cncItem = await _orderCncItemRepository.FindAsync(t => t.SubOrderId == subOrder.Id);
                    return GetSubOrderFile(
                        cncItem.FileName,
                        cncItem.FilePath,
                        false,
                        null,
                        EnumPurchasedParts.Unwanted);
                case EnumOrderType.SheetMetal:
                    var sheetMetalItem = await _orderSheetMetalItemRepository.FindAsync(t => t.SubOrderId == subOrder.Id);
                    return GetSubOrderFile(
                        sheetMetalItem.FileName,
                        sheetMetalItem.FilePath,
                        sheetMetalItem.NeedDesign,
                        sheetMetalItem.ProcessParameters,
                        sheetMetalItem.PurchasedParts);
            }
            return null;
        }
        private SubOrderFile GetSubOrderFile(
            string filename,
            string filePath,
            bool needDesign,
            string processParameters,
            EnumPurchasedParts purchasedParts)
        {
            return new SubOrderFile()
            {
                FilePath = filePath,
                FileName = filename,
                NeedDesign = needDesign,
                ProcessParameters = processParameters,
                PurchasedParts = purchasedParts
            };
        }
    }
}
