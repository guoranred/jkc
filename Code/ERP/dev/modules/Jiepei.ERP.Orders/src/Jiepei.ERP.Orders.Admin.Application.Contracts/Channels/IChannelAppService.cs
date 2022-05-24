using Jiepei.ERP.Orders.Channels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Channels
{
    public interface IChannelAppService : IApplicationService
    {
        Task<ChannelDto> GetAsync(Guid id);

        Task<List<ChannelDto>> GetListAsync(List<Guid> id);
        Task<bool> IsEnableAsnyc(Guid id);

        Task PutEnableAsnyc(Guid id, bool isEnable);

        Task<Guid> CreateAsync(CreateChannelDto input);

        Task<List<ChannelDto>> GetListAsync(string type);
    }
}
