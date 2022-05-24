using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Admin.HttpApi;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ERP.Shared.Consumers.Orders.SubOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Orders.SubOrders
{
    /// <summary>
    /// 
    /// </summary>
    [RemoteService(Name = OrdersAdminRemoteServiceConsts.RemoteServiceName)]
    [Route("api/orders/sub-order")]
    public class SubOrderController : OrdersAdminController
    {
        private readonly ISubOrderAppService _subOrderAppService;
        private readonly ISubOrderExtraAppService _subOrderExtra;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subOrderAppService"></param>
        /// <param name="subOrderExtra"></param>
        public SubOrderController(ISubOrderAppService subOrderAppService
           , ISubOrderExtraAppService subOrderExtra)
        {
            _subOrderAppService = subOrderAppService;
            _subOrderExtra = subOrderExtra;
        }



        /// <summary>
        /// 交期变更
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/deliverydays-change")]
        public async Task<IActionResult> DeliveryDaysChange(Guid id, DeliveryDaysInput input)
        {
            await _subOrderAppService.DeliveryDays(id, input);
            return NoContent();
        }

        #region Extra

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/app/sub-order/mold-task-exter")]
        //[RemoteService(false)]
        public async Task<bool> MoldTaskExterAsync(MQ_Mold_OrderTaskDto input)
        {
            return await _subOrderExtra.MoldTaskExterAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/app/sub-order/injection-task-exter")]
        //[RemoteService(false)]
        public async Task<bool> InjectionTaskExterAsync(MQ_Injection_OrderTaskDto input)
        {
            return await _subOrderExtra.InjectionTaskExterAsync(input);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/app/sub-order/cnc-task-exter")]
        [AllowAnonymous]
        //[RemoteService(false)]
        public async Task<bool> CncTaskExterAsync(MQ_Cnc_OrderTaskDto input)
        {
            return await _subOrderExtra.CncTaskExterAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/api/app/sub-order/cancel-exter")]
        //[RemoteService(false)]
        public async Task<bool> CancelExterAsync(MQSubOrderCancelDto input)
        {
            return await _subOrderExtra.CancelExterAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/api/app/sub-order/payment-exter")]
        //[RemoteService(false)]
        public async Task PaymentExterAsync(MQSubOrderPaymentDto input)
        {
            await _subOrderExtra.PaymentExterAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/api/app/sub-order/receive-exter")]
        //[RemoteService(false)]
        public async Task<bool> ReceiveExterAsync(MQSubOrderReceiveDto input)
        {
            return await _subOrderExtra.ReceiveExterAsync(input);
        }
        #endregion
        /*
                #region Mold
                [HttpPost]
                [Route("~/api/app/mold-order/task-exter")]

                public async Task<bool> MoldTaskExterAsync(MQ_Mold_OrderTaskDto input)
                {
                    return await _moldOrder.PostTaskExterAsync(input);
                }
                [HttpPut]
                [Route("~/api/app/mold-order/cancel-exter")]
                [RemoteService(false)]
                public async Task<bool> MoldCancelExterAsync(MQ_Mold_OrderCancelDto input)
                {
                    return await _moldOrder.PutCancelExterAsync(input);
                }

                [HttpPut]
                [Route("~/api/app/mold-order/payment-exter")]
                [RemoteService(false)]
                public async Task MoldPaymentExterAsync(MQ_Mold_OrderPaymentDto input)
                {
                    await _moldOrder.PutPaymentExterAsync(input);
                }
                [HttpPut]
                [Route("~/api/app/mold-order/receive-exter")]
                [RemoteService(false)]
                public async Task<bool> MoldReceiveExterAsync(MQ_Mold_OrderReceiveDto input)
                {
                    return await _moldOrder.PutReceiveExterAsync(input);
                }
                #endregion

                #region Injection
                [HttpPost]
                [Route("~/api/app/injection-order/task-exter")]
                [RemoteService(false)]
                public async Task<bool> InjectionTaskExterAsync(MQ_Injection_OrderTaskDto input)
                {
                    return await _injectionOrder.PostTaskExterAsync(input);
                }
                [HttpPut]
                [Route("~/api/app/injection-order/cancel-exter")]
                [RemoteService(false)]
                public async Task<bool> InjectionCancelExterAsync(MQ_Injection_OrderCancelDto input)
                {
                    return await _injectionOrder.PutCancelExterAsync(input);
                }

                [HttpPut]
                [Route("~/api/app/injection-order/payment-exter")]
                [RemoteService(false)]
                public async Task InjectionPaymentExterAsync(MQ_Injection_OrderPaymentDto input)
                {
                    await _injectionOrder.PutPaymentExterAsync(input);
                }
                [HttpPut]
                [Route("~/api/app/injection-order/receive-exter")]
                [RemoteService(false)]
                public async Task<bool> InjectionReceiveExterAsync(MQ_Injection_OrderReceiveDto input)
                {
                    return await _injectionOrder.PutReceiveExterAsync(input);
                }
                #endregion

                #region Cnc
                [HttpPost]
                [Route("~/api/app/cnc-order/task-exter")]
                [RemoteService(false)]
                public async Task<bool> CncTaskExterAsync(MQ_Cnc_OrderTaskDto input)
                {
                    return await _cncOrder.PostTaskExterAsync(input);
                }
                [HttpPut]
                [Route("~/api/app/cnc-order/cancel-exter")]
                [RemoteService(false)]
                public async Task<bool> CncCancelExterAsync(MQ_Cnc_OrderCancelDto input)
                {
                    return await _cncOrder.PutCancelExterAsync(input);
                }

                [HttpPut]
                [Route("~/api/app/cnc-order/payment-exter")]
                [RemoteService(false)]
                public async Task CncPaymentExterAsync(MQ_Cnc_OrderPaymentDto input)
                {
                    await _cncOrder.PutPaymentExterAsync(input);
                }
                [HttpPut]
                [Route("~/api/app/cnc-order/receive-exter")]
                [RemoteService(false)]
                public async Task<bool> CncReceiveExterAsync(MQ_Cnc_OrderReceiveDto input)
                {
                    return await _cncOrder.PutReceiveExterAsync(input);
                }
                #endregion
        */
    }
}
