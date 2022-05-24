using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Channels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Orders.Channels
{
    /// <summary>
    /// 渠道
    /// </summary>
    public class ChannelAppService : OrdersAdminAppServiceBase, IChannelAppService
    {
        private readonly IChannelRepository _channelRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelRepository"></param>
        public ChannelAppService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        /// <summary>
        /// 创建渠道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> CreateAsync(CreateChannelDto input)
        {
            var entity = ObjectMapper.Map<CreateChannelDto, Channel>(input);
            entity.IsEnable = false;
            var channel = await _channelRepository.InsertAsync(entity);
            return channel.Id;
        }


        /// <summary>
        /// 获取渠道列表信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChannelDto>> GetListAsync(string type)
        {
            var channelList = new List<Channel>();
            if (type== "all")
            {
                channelList = await _channelRepository.GetListAsync();
            }
            else
            {
                channelList = await _channelRepository.GetListAsync(t => t.IsEnable == true);
            }
            return ObjectMapper.Map<List<Channel>,List<ChannelDto>>(channelList);
        }

        /// <summary>
        /// 获取渠道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChannelDto> GetAsync(Guid id)
        {
            var channel = await _channelRepository.FindAsync(x => x.Id == id);
            return ObjectMapper.Map<Channel, ChannelDto>(channel);
        }

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ChannelDto>> GetListAsync(List<Guid> id)
        {
            var channeldtoList = new List<ChannelDto>();
            var channelList = await _channelRepository.GetListAsync(x => id.Contains(x.Id));
            foreach (var item in channelList)
            {
                channeldtoList.Add(ObjectMapper.Map<Channel, ChannelDto>(item));
            }
            return channeldtoList;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsEnableAsnyc(Guid id)
        {
            var channel = await _channelRepository.FindAsync(x => x.Id == id);
            return channel?.IsEnable ?? false;
        }

        /// <summary>
        /// 设置渠道启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public async Task PutEnableAsnyc(Guid id, bool isEnable)
        {
            var entity = await _channelRepository.FindAsync(x => x.Id == id);
            if (entity == null)
                throw new UserFriendlyException("渠道不存在");

            entity.IsEnable = isEnable;
            await _channelRepository.UpdateAsync(entity);
        }
    }
}
