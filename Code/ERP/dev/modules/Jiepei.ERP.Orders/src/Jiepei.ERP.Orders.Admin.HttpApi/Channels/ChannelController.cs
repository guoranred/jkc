using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Admin.HttpApi;
using Jiepei.ERP.Orders.Channels.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Orders.Channels
{
    /// <summary>
    /// 
    /// </summary>
    [RemoteService(Name = OrdersAdminRemoteServiceConsts.RemoteServiceName)]
    [Route("api/orders/channel")]
    public class ChannelController : OrdersAdminController
    {
        private readonly IChannelAppService _channelAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelAppService"></param>
        public ChannelController(IChannelAppService channelAppService)
        {
            _channelAppService = channelAppService;
        }

        /// <summary>
        /// 创建渠道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> CreateAsync(CreateChannelDto input)
        {
            return await _channelAppService.CreateAsync(input);
        }

        /// <summary>
        /// 获取渠道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ChannelDto> GetAsync(Guid id)
        {
            return await _channelAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取渠道列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<ChannelDto>> GetListAsync()
        {
            return await _channelAppService.GetListAsync("");
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/isenable")]
        public async Task<bool> IsEnableAsnyc(Guid id)
        {
            return await _channelAppService.IsEnableAsnyc(id);
        }

        /// <summary>
        /// 设置渠道启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        [HttpPut("{id}/enable")]
        public async Task PutEnableAsnyc(Guid id, bool isEnable)
        {
            await _channelAppService.PutEnableAsnyc(id, isEnable);
        }
    }
}
