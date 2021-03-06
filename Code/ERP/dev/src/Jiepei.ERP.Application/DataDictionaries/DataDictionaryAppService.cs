using EasyAbp.Abp.DataDictionary;
using EasyAbp.Abp.DataDictionary.Dtos;
using EasyAbp.Abp.DataDictionary.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.DataDictionaries
{
    [Authorize(DataDictionaryPermissions.DataDictionary.Default)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IDataDictionaryAppService), typeof(IDataDictionaryAppService), typeof(DataDictionaryAppService))]
    public class DataDictionaryAppService : ApplicationService, IDataDictionaryAppService
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly IDataDictionaryManager _dataDictionaryManager;
        private readonly IDataFilter _dataFilter;
        public DataDictionaryAppService(IDataDictionaryRepository dataDictionaryRepository,
            IDataDictionaryManager dataDictionaryManager, IDataFilter dataFilter)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _dataDictionaryManager = dataDictionaryManager;
            _dataFilter = dataFilter;
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Create)]
        public async Task<DataDictionaryDto> CreateAsync(DataDictionaryCreateDto input)
        {
            var newDict = new DataDictionary(GuidGenerator.Create(),
                CurrentTenant.Id,
                input.Code,
                input.DisplayText,
                input.Description,
                new List<DataDictionaryItem>(),
                input.IsStatic);

            foreach (var itemDto in input.Items)
            {
                newDict.AddOrUpdateItem(itemDto.Code, itemDto.DisplayText, itemDto.Description);
            }

            await _dataDictionaryManager.CreateAsync(newDict);

            return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(newDict);
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Update)]
        public async Task<DataDictionaryDto> UpdateAsync(Guid id, DataDictionaryUpdateDto input)
        {
                var dict = await _dataDictionaryRepository.GetAsync(id);

                dict.SetContent(input.DisplayText, input.Description);

                foreach (var itemDto in input.Items)
                {
                    dict.AddOrUpdateItem(itemDto.Code, itemDto.DisplayText, itemDto.Description);
                }

                dict.Items.RemoveAll(item => !input.Items.Select(dtoItem => dtoItem.Code).Contains(item.Code));

                await _dataDictionaryManager.UpdateAsync(dict);

                return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(dict);
            
        }

        [Authorize(DataDictionaryPermissions.DataDictionary.Delete)]
        public Task DeleteAsync(Guid id)
        {
                return _dataDictionaryRepository.DeleteAsync(id);
           
        }

        public async Task<DataDictionaryDto> GetAsync(Guid id)
        {
                var dict = await _dataDictionaryRepository.GetAsync(id);
                return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(dict);
          
        }

        public async Task<PagedResultDto<DataDictionaryDto>> GetListAsync(PagedResultRequestDto input)
        {
                var totalCount = await _dataDictionaryRepository.GetCountAsync();
                var resultList = await _dataDictionaryRepository.GetListAsync(input.SkipCount, input.MaxResultCount);
                return new PagedResultDto<DataDictionaryDto>(totalCount, ObjectMapper.Map<List<DataDictionary>, List<DataDictionaryDto>>(resultList));
        }

        [AllowAnonymous]
        public async Task<DataDictionaryDto> FindByCodeAsync(string code)
        {
            var dict = await _dataDictionaryRepository.FindAsync(d => d.Code == code);

            return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(dict);
        }
    }
}
