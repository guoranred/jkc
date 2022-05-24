using Jiepei.ERP.Orders.Pays.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiepei.ERP.Orders.Pays
{
    [AllowAnonymous]
    public class OrderPayLogAppService : OrdersAppService, IOrderPayLogAppService
    {
        private readonly IOrderPayLogManager _orderPayLogManager;
        private readonly IOrderPayLogRepository _orderPayLogRepository;
        private readonly IOrderPayDetailLogRepository _orderPayDetailLogRepository;
        public OrderPayLogAppService(IOrderPayLogManager orderPayLogManager
            , IOrderPayLogRepository orderPayLogRepository
            , IOrderPayDetailLogRepository orderPayDetailLogRepository)
        {
            _orderPayLogManager = orderPayLogManager;
            _orderPayLogRepository = orderPayLogRepository;
            _orderPayDetailLogRepository = orderPayDetailLogRepository;
        }

        /// <summary>
        /// 创建支付日志【日志、详细日志】
        /// </summary>
        /// <param name="log"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        [Authorize]
        public async Task CreateAsync(CreateOrderPayLogDto log, List<CreateOrderPayDetailLogDto> details)
        {
            var logEntity = ObjectMapper.Map<CreateOrderPayLogDto, OrderPayLog>(log);
            var detailLogs = new List<OrderPayDetailLog>();
            foreach (var detail in details ?? new List<CreateOrderPayDetailLogDto>())
            {
                var detailEntity = ObjectMapper.Map<CreateOrderPayDetailLogDto, OrderPayDetailLog>(detail);
                detailLogs.Add(detailEntity);
            }

            await _orderPayLogManager.CreateAsync(logEntity, detailLogs);
        }

        /// <summary>
        /// 根据PayCode查询支付日志
        /// </summary>
        /// <param name="payCode"></param>
        /// <returns></returns>
        public async Task<GetOrderPayLogDto> GetByPayCodeAsync(string payCode)
        {
            var log = await _orderPayLogRepository.FindAsync(x => x.PayCode.Equals(payCode));
            return ObjectMapper.Map<OrderPayLog, GetOrderPayLogDto>(log);
        }

        public async Task UpdatePayLogAsync(string payCode, bool isSucess)
        {
            await _orderPayLogManager.UpdateAsync(payCode, isSucess);
        }

        /// <summary>
        /// 根据支付日志Id查询日志详情列表
        /// </summary>
        /// <param name="payLogId"></param>
        /// <returns></returns>
        public async Task<List<GetOrderPayDetailLogDto>> GetDetailListAsync(Guid payLogId)
        {
            var details = await _orderPayDetailLogRepository.GetListAsync(x => x.PayLogId.Equals(payLogId));

            var list = new List<GetOrderPayDetailLogDto>();
            foreach (var item in details ?? new List<OrderPayDetailLog>())
            {
                list.Add(ObjectMapper.Map<OrderPayDetailLog, GetOrderPayDetailLogDto>(item));
            }
            return list;
        }
    }
}
