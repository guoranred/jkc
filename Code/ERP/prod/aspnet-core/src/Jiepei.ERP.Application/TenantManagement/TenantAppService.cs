
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Saas.Host.Dtos;
using Volo.Saas.Tenants;

namespace Jiepei.ERP.TenantManagement
{
    public class AbpTenantAppService : ERPAppService
    {
        private readonly ITenantRepository _tenantRepository;
        public AbpTenantAppService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<IEnumerable<TenantDto>> GetAllAsync()
        {
            var entities = await _tenantRepository.GetListAsync();
            var dtos = new List<TenantDto>();
            foreach (var entity in entities)
            {
                dtos.Add(new TenantDto { Id = entity.Id, Name = entity.Name });
            }
            return dtos;
        }

        public class TenantDto : EntityDto<Guid>
        {
            public string Name { get; set; }
        }
    }
}
