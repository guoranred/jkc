using Jiepei.ERP.Members.CustomerServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members.CustomerServices
{
    public interface IConsultantServiceAppService : IApplicationService
    {
        Task<CustomerServiceDto> GetAsync(Guid id);
    }
}
