using Jiepei.ERP.Members.Enums;
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Jiepei.ERP.Members.Admin.Members
{
    /// <summary>
    /// 会员信息管理
    /// </summary>
    public class MemberManagementAppService : MembersAdminAppService, IMemberManagementAppService
    {
        private readonly IMemberInformationRepository _memberInfomationRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IChannelAppService _channelAppService;

        private readonly IDataFilter _dataFilter;

        public MemberManagementAppService(IMemberInformationRepository memberInfomationRepository, IDataFilter dataFilter, ICustomerServiceRepository customerServiceRepository, IChannelAppService channelAppService)
        {
            _memberInfomationRepository = memberInfomationRepository;
            _dataFilter = dataFilter;
            _customerServiceRepository = customerServiceRepository;
            _channelAppService = channelAppService;
        }

        /// <summary>
        /// 分页获取会员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MemberInfomationDto>> GetListAsync(MemberInfomationQueryInput input)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var members = await _memberInfomationRepository.GetPagedListAsync(input.Name,
                                                                                  input.Code,
                                                                                  input.Phone,
                                                                                  input.CustomerServiceId,
                                                                                  input.SalesmanId,
                                                                                  input.Sorting,
                                                                                  input.MaxResultCount,
                                                                                  input.SkipCount);

                var totalCount = await _memberInfomationRepository.CountAsync(input.Name,
                                                                              input.Code,
                                                                              input.Phone,
                                                                              input.CustomerServiceId,
                                                                              input.SalesmanId);

                var memberInfomationDtos = ObjectMapper.Map<List<MemberInformation>, List<MemberInfomationDto>>(members);

                var salesmans = await _customerServiceRepository.GetListAsync(t => t.Type == CustomerServiceTypeEnum.SalesMan || t.Type == CustomerServiceTypeEnum.CustomerService);
                var customerService = await _customerServiceRepository.GetListAsync(t => t.Type == CustomerServiceTypeEnum.CustomerService);
                var channelList = await _channelAppService.GetListAsync("");

                memberInfomationDtos.ForEach(t =>
                {
                    t.SalesmanName = salesmans.Where(s => s.Id == t.SalesmanId).Select(s => s.Name).FirstOrDefault();

                    t.CustomerServiceName = customerService
                    .Where(s => s.Id == t.CustomerServiceId)
                    .Select(s => s.Name).FirstOrDefault();

                    t.Source = t.Source.IsNullOrWhiteSpace() ? "自然注册" : salesmans
                                                                            .Where(s => s.PromoCode == t.Source)
                                                                            .Select(s => s.Type.GetDescriptionV2() + "-" + s.Name)
                                                                            .FirstOrDefault() ?? "自然注册";

                    t.ChannelName = channelList.Where(o => o.Id == t.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                });

                return new PagedResultDto<MemberInfomationDto>(totalCount, memberInfomationDtos);
            }
        }

        /// <summary>
        /// 根据主键Guid获取会员详细信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public async Task<MemberInformationDetailDto> GetAsync(Guid id)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var memberInformationDetailDto= ObjectMapper.Map<MemberInformation, MemberInformationDetailDto>(
                await _memberInfomationRepository.FindAsync(id));

                var channelList = await _channelAppService.GetListAsync("");
                memberInformationDetailDto.ChannelName = channelList.Where(o => o.Id == memberInformationDetailDto.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                return memberInformationDetailDto;
            }
        }

        /// <summary>
        /// 根据主键Guid修改会员信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="inputDto">修改内容</param>
        /// <returns></returns>
        public async Task<MemberInfomationDto> UpdateAsync(Guid id, UpdateMemberInformationDto inputDto)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var memberInformation = await _memberInfomationRepository.GetAsync(id);
                var model = ObjectMapper.Map(inputDto, memberInformation);
                var memberInfomationDto= ObjectMapper.Map<MemberInformation, MemberInfomationDto>(await _memberInfomationRepository.UpdateAsync(model));

                var channelList = await _channelAppService.GetListAsync("");
                memberInfomationDto.ChannelName = channelList.Where(o => o.Id == memberInfomationDto.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                return memberInfomationDto;
            }
        }

        /// <summary>
        /// 根据主键Guid修改客服
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<MemberInfomationDto> UpdateCustomerServicetAsync(Guid id, UpdateMemberInfomationConsultantDto inputDto)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var memberInformation = await _memberInfomationRepository.GetAsync(id, true);
                var customerService = await _customerServiceRepository.GetAsync(inputDto.CustomerServiceId);
                memberInformation.CustomerServiceId = customerService.Id;
                var memberInfomationDto= ObjectMapper.Map<MemberInformation, MemberInfomationDto>(memberInformation);

                var channelList = await _channelAppService.GetListAsync("");
                memberInfomationDto.ChannelName = channelList.Where(o => o.Id == memberInfomationDto.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                return memberInfomationDto;
            }
        }

        /// <summary>
        /// 根据主键Guid变更会员账号状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<MemberInfomationDto> UpdateAccountStatusAsync(Guid id, UpdateMemberInformationAccountStatusDto inputDto)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var memberInformation = await _memberInfomationRepository.GetAsync(id);
                memberInformation.IsDeleted = inputDto.Status;
                var memberInfomationDto= ObjectMapper.Map<MemberInformation, MemberInfomationDto>(memberInformation);

                var channelList = await _channelAppService.GetListAsync("");
                memberInfomationDto.ChannelName = channelList.Where(o => o.Id == memberInfomationDto.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                return memberInfomationDto;
            }
        }

        /// <summary>
        /// 根据主键Guid修改会员账户的密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<MemberInfomationDto> UpdatePasswordAsync(Guid id, UpdateMemberInformationPasswordDto inputDto)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var memberInformation = await _memberInfomationRepository.GetAsync(id);
                inputDto.Password = GetMD5Str(inputDto.Password);
                var model = ObjectMapper.Map(inputDto, memberInformation);
                var memberInfomationDto=ObjectMapper.Map<MemberInformation, MemberInfomationDto>(await _memberInfomationRepository.UpdateAsync(model));

                var channelList = await _channelAppService.GetListAsync("");
                memberInfomationDto.ChannelName = channelList.Where(o => o.Id == memberInfomationDto.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                return memberInfomationDto;
            }
        }

        /// <summary>
        /// 根据主键Guid删除会员信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _memberInfomationRepository.DeleteAsync(id);
        }

        public async Task<List<MemberInfomationDto>> GetListByIdsAsync(Guid[] ids)
        {
            var reuslt = await _memberInfomationRepository.GetListAsync(t => ids.Contains(t.Id));
           var memberInfomationDtoList= ObjectMapper.Map<List<MemberInformation>, List<MemberInfomationDto>>(reuslt);

            var channelList = await _channelAppService.GetListAsync("");
            foreach (var item in memberInfomationDtoList)
            {
                item.ChannelName = channelList.Where(o => o.Id == item.ChannelId).Select(s => s.ChannelName).FirstOrDefault();
            }
            return memberInfomationDtoList;
        }

        /// <summary>
        /// 根据主键Guid修改业务员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<MemberInfomationDto> UpdateSalesmanAsync(Guid id, UpdateMemberInfomationSalesmanDto inputDto)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var memberInformation = await _memberInfomationRepository.GetAsync(id);
                var customerService = await _customerServiceRepository.GetAsync(inputDto.SalesmanId);
                memberInformation.SalesmanId = customerService.Id;
                var memberInfomationDto= ObjectMapper.Map<MemberInformation, MemberInfomationDto>(memberInformation);

                var channelList = await _channelAppService.GetListAsync("");
                memberInfomationDto.ChannelName = channelList.Where(o => o.Id == memberInfomationDto.ChannelId).Select(s => s.ChannelName).FirstOrDefault();

                return memberInfomationDto;
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        ///
        protected string GetMD5Str(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var newPwd = BitConverter.ToString(result).Replace("-", "");

            return newPwd;
        }
    }
}
