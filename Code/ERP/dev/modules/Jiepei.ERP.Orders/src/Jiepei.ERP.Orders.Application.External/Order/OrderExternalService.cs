using Jiepei.ERP.Commons;
using Jiepei.ERP.Orders.Application.External.Cache;
using Jiepei.ERP.Orders.Application.External.Configuration;
using Jiepei.ERP.Orders.Application.External.Order.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Jiepei.ERP.Orders.Application.External.Order
{
    /// <summary>
    /// 订单信息相关外部服务
    /// 该部门接口与第三方定义的接口相对应
    /// 参数必须为第三方定义的结构  方便日志记录
    /// </summary>
   // [RemokeInvokeLog]
    public class OrderExternalService : ISheetMetalCallMethodInformation
    {
        /// <summary>
        /// 钣金站调用远程服务帮助类
        /// </summary>
        private readonly SheetMetalServiceHelper _sheetMetalServiceHelper;

        /// <summary>
        /// 钣金站远程服务配置
        /// </summary>
        private readonly SheetMetalApiConfiguration _apiConfiguration;

        /// <summary>
        /// 调用地址
        /// </summary>
        public string CallUrl { get; set; }

        /// <summary>
        /// 发送的参数
        /// </summary>
        public string CallBody { get; set; }


        public OrderExternalService(SheetMetalServiceHelper sheetMetalServiceHelper,
            SheetMetalApiConfiguration apiConfiguration
        )
        {
            _sheetMetalServiceHelper = sheetMetalServiceHelper;
            _apiConfiguration = apiConfiguration;
        }

        /// <summary>
        /// 赋值于ISheetMetalCallMethodInformation接口，用于日志记录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        private void SetCallMethodInformation(string url, string body = null)
        {
            CallUrl = url;
            CallBody = body;
        }

        /// <summary>
        /// 不定义完整Url则使用默认地址拼接
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string DefaultUrlHandle(string url)
        {
            if (!url.StartsWith("http"))
                url = _apiConfiguration.RemoteAddress + url;
            return url;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">不定义完整Url则使用默认地址</param>
        /// <param name="body">请求参数</param>
        /// <param name="method">请求类型</param>
        /// <returns></returns>
        private async Task<ApiHttpResponse> SendSheetMetalAsync(string url, string body = null, EnumHttpMethod method = EnumHttpMethod.Post)
        {
            url = DefaultUrlHandle(url);
            SetCallMethodInformation(url, body);
            switch (method)
            {
                case EnumHttpMethod.Get:
                    return await _sheetMetalServiceHelper.SheetMetalGetAsync(url);
                case EnumHttpMethod.Post:
                    return await _sheetMetalServiceHelper.SheetMetalPostAsync(url, body);
                default:
                    return new ApiHttpResponse($"无法处理的请求方式，请求方式为：{method}");
            }
        }

        /// <summary>
        /// 同步订单至订单中心
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> SyncOrderInfoAsync(CreateOrderDto createOrderDto)
        {
            ApiHttpResponse apiResult;
            if (null == createOrderDto)
            {
                apiResult = new ApiHttpResponse("同步的订单信息不能为空!");
            }
            else
            {
                var body = JsonConvert.SerializeObject(createOrderDto);
                apiResult = await SendSheetMetalAsync("/api/OrderInfo/CreateOrder", body);
            }
            return apiResult;
        }

        /// <summary>
        /// 同步订单包至订单中心
        /// </summary>
        /// <param name="orderGroup"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> SyncOrderGroupAsync(OrderGroupDto orderGroup)
        {
            ApiHttpResponse apiResult;
            if (null == orderGroup)
            {
                apiResult = new ApiHttpResponse("同步的订单包信息不能为空!");
            }
            else
            {
                var body = JsonConvert.SerializeObject(orderGroup);
                apiResult = await SendSheetMetalAsync("/api/OrderGroup/SaveOrUpdate", body);
            }
            return apiResult;
        }

        /// <summary>
        /// 取消订单中心的订单
        /// </summary>
        /// <param name="cancelOrder"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> CancelOrderInfoAsync(CancelOrderDto cancelOrder)
        {
            ApiHttpResponse apiResult;
            if (null == cancelOrder)
            {
                apiResult = new ApiHttpResponse("取消订单的信息不能为空");
            }
            else
            {
                var body = JsonConvert.SerializeObject(cancelOrder);
                apiResult = await SendSheetMetalAsync("/api/OrderInfo/CancelOrder", body);
            }
            return apiResult;
        }

        /// <summary>
        /// 修改订单执行状态   确认下单之后,应该是正常执行还是暂停执行(暂停执行则订单生产会暂停)
        /// </summary>
        /// <param name="updateOrderStopExec"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> ModifyOrderExecutionStatusAsync(UpdateOrderStopExecDto updateOrderStopExec)
        {
            ApiHttpResponse apiResult;
            if (null == updateOrderStopExec)
            {
                apiResult = new ApiHttpResponse("修改订单执行的信息为空");
            }
            else
            {
                var body = JsonConvert.SerializeObject(updateOrderStopExec);
                apiResult = await SendSheetMetalAsync("/api/OrderInfo/ModifyOrderExecutionStatus", body);
            }
            return apiResult;
        }

        /// <summary>
        /// 订单支付确认
        /// </summary>
        /// <param name="updateOrderPayStatus"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> ModifyOrderPayStatusAsync(UpdateOrderPayStatusDto updateOrderPayStatus)
        {
            ApiHttpResponse apiResult;
            if (null == updateOrderPayStatus)
            {
                apiResult = new ApiHttpResponse("修改订单支付状态的信息为空");
            }
            else
            {
                var body = JsonConvert.SerializeObject(updateOrderPayStatus);
                apiResult = await SendSheetMetalAsync("/api/OrderInfo/ModifyOrderPayStatus", body);
            }
            return apiResult;
        }

        /// <summary>
        /// 修改订单文件信息
        /// </summary>
        /// <param name="updateOrderProductFile"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> ModifyOrderProductFileAsync(UpdateOrderProductFileDto updateOrderProductFile)
        {
            ApiHttpResponse apiResult;
            if (null == updateOrderProductFile)
            {
                apiResult = new ApiHttpResponse("修改订单文件的信息为空");
            }
            else
            {
                var body = JsonConvert.SerializeObject(updateOrderProductFile);
                apiResult = await SendSheetMetalAsync("/api/OrderInfo/ModifyOrderProductFile", body);
            }
            return apiResult;
        }


        /// <summary>
        /// 用户完结订单
        /// </summary>
        /// <param name="updateOrderProductFile"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> OrderFinishAsync(string OrderCode)
        {
            ApiHttpResponse apiResult;
            if (string.IsNullOrWhiteSpace(OrderCode))
            {
                apiResult = new ApiHttpResponse("订单号为空");
            }
            else
            {
                var body = JsonConvert.SerializeObject(new { orderNo = OrderCode });
                apiResult = await SendSheetMetalAsync("/api/OrderInfo/OrderFinish", body);
            }
            return apiResult;
        }

        /// <summary>
        /// 根据订单ID从订单中心获取订单信息  该ID来自于OrderInfo的ExternalOrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> GetOrderInfoByIdAsync(int orderId)
        {
            string url = "/api/OrderInfo/GetByOrderId?orderId=" + orderId;
            var apiResult = await SendSheetMetalAsync(url, method: EnumHttpMethod.Get);
            apiResult.Data = apiResult.Data?.ToString();
            return apiResult;
        }

        /// <summary>
        /// 根据订单编号从订单中心获取订单信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> GetOrderInfoByNoAsync(string orderNo)
        {
            string url = "/api/OrderInfo/GetByOrderNo?orderNo=" + orderNo;
            var apiResult = await SendSheetMetalAsync(url, method: EnumHttpMethod.Get);
            apiResult.Data = apiResult.Data?.ToString();
            return apiResult;
        }

        /// <summary>
        /// 根据订单编号从订单中心获取订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> GetOrderInfoDetailByNoAsync(string orderNo)
        {
            string url = "/api/OrderInfo/GetFullByOrderNo?orderNo=" + orderNo;
            var apiResult = await SendSheetMetalAsync(url, method: EnumHttpMethod.Get);
            apiResult.Data = apiResult.Data?.ToString();
            return apiResult;
        }

        /// <summary>
        ///获取在线计价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> GetProductPriceAsync(ProductPriceInputDto input)
        {
            ApiHttpResponse apiResult;
            if (null == input)
            {
                apiResult = new ApiHttpResponse("在线计价信息不能为空");
            }
            else
            {
                var body = JsonConvert.SerializeObject(input);
                apiResult = await SendSheetMetalAsync(_apiConfiguration.PriceRemoteAddress + "/api/Pricing/Pricing", body);

            }
            return apiResult;
        }

        /// <summary>
        /// 在线计价获取初始化参数
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ApiHttpResponse> GetMaterialInitListForFrontAsync()
        {
            string url = _apiConfiguration.PriceRemoteAddress + "/api/Pricing/GetMaterialInitListForFront";
            var apiResult = await SendSheetMetalAsync(url, method: EnumHttpMethod.Get);
            apiResult.Data = apiResult.Data?.ToString();
            return apiResult;
        }
    }
}
