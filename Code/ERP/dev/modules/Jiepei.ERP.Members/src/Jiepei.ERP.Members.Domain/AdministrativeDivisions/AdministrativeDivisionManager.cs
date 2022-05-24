using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Members.AdministrativeDivisions
{
    public class AdministrativeDivisionManager : DomainService, IAdministrativeDivisionManager
    {
        private readonly IAdministrativeDivisionRepository _administrativeDivisions;

        public AdministrativeDivisionManager(IAdministrativeDivisionRepository administrativeDivisions)
        {
            _administrativeDivisions = administrativeDivisions;
        }

        public async Task<AdministrativeDivision> CreateAsync(AdministrativeDivision input)
        {
            await CheckCodeExist(input.Code);
            if (input.Pid != null)
                await CheckPIdExist(input.Pid.Value);
            return await _administrativeDivisions.InsertAsync(input);
        }

        public async Task<AdministrativeDivision> UpdateAsync(AdministrativeDivision input)
        {
            await CheckCodeExist(input.Id, input.Code);
            if (input.Pid != null)
                await CheckPIdExist(input.Pid.Value);
            return await _administrativeDivisions.UpdateAsync(input);
        }

        private async Task CheckCodeExist(string code)
        {
            var existValue = await _administrativeDivisions.FindAsync(t => t.Code == code);
            if (existValue != null)
            {
                throw new UserFriendlyException($"地区编号：{code}已经存在");
            }
        }

        private async Task CheckCodeExist(Guid id, string code)
        {
            var existValue = await _administrativeDivisions.FindAsync(t => t.Id != id && t.Code == code);
            if (existValue != null)
            {
                throw new UserFriendlyException($"地区编号：{code}已经存在");
            }
        }

        public async Task CheckPIdExist(Guid id)
        {
            var existValue = await _administrativeDivisions.FindAsync(x => x.Id == id);
            if (existValue == null)
            {
                throw new UserFriendlyException($"父地区信息ID：{id}不存在");
            }
        }
    }
}
