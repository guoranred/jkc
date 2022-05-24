using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Admin
{
    public interface IThreeDPrintLogisticsAppService : IApplicationService
    {
        Task<Dictionary<string, long>> GetThreeDPrintLogisticsCountByStausAsync();
        Task<PagedResultDto<ThreeDPrintLogisticsDto>> GetThreeDPrintLogisticsListAsync(GetThreeDPrintLogisticsInput input);
        Task<IEnumerable<SubOrderThreeDItemDto>> GetThreeDPrintBomList(string orderNo);
        Task ChangeInboundNumAsync(List<ChangeInboundNumInput> inputs);
        Task DeliverAsync(Guid id, DeliverDto input);
    }
}
