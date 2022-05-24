using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Channels
{
    public interface IChannelRepository : IRepository<Channel, Guid>
    {
    }
}
