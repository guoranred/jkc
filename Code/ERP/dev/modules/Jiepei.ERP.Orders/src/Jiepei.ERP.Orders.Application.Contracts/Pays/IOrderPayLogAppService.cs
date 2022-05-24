using Jiepei.ERP.Orders.Pays.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Pays
{
    public interface IOrderPayLogAppService : IApplicationService
    {
        Task<GetOrderPayLogDto> GetByPayCodeAsync(string payCode);

        Task UpdatePayLogAsync(string payCode, bool isSucess);

        Task<List<GetOrderPayDetailLogDto>> GetDetailListAsync(Guid payLogId);

        Task CreateAsync(CreateOrderPayLogDto log, List<CreateOrderPayDetailLogDto> details);
    }
}
