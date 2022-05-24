using Jiepei.ERP.Orders.Channels.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Jiepei.ERP.Orders.Channels
{
    [Authorize]
    public class ChannelAppService : OrdersAppService, IChannelAppService
    {
        private readonly IChannelRepository _channelRepository;
        public ChannelAppService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }
        [AllowAnonymous]
		public async Task<GetChannelDto> GetAsync(Guid id)
        {
            var entity = await _channelRepository.FindAsync(x => x.Id == id);
            return ObjectMapper.Map<Channel, GetChannelDto>(entity);
        }
    }
}
