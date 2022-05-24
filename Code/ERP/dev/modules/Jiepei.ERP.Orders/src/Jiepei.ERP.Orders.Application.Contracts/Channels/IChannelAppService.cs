using Jiepei.ERP.Orders.Channels.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Channels
{
    public interface IChannelAppService : IApplicationService
    {
        Task<GetChannelDto> GetAsync(Guid id);
    }
}
