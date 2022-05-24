using Jiepei.ERP.Orders.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin.Application.Banners
{
    /// <summary>
    /// Banner
    /// </summary>
    public class BannerManagementAppService : NewsAdminAppService, IBannerManagementAppService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IChannelAppService _channelAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bannersRepository"></param>
        public BannerManagementAppService(IBannerRepository bannersRepository
            , IChannelAppService channelAppService)
        {
            _bannerRepository = bannersRepository;
            _channelAppService = channelAppService;
        }
        /// <summary>
        /// 分页获取Banner列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BannerManagementDto>> GetListAsync(BannerManagementQueryDto input)
        {
            var expression = CommonExpression();
            if (!string.IsNullOrEmpty(input.Title))
            {
                expression = expression.And(o => o.Title.Contains((input.Title)));
            }
            IOrderedQueryable<Banner> orderBy(IQueryable<Banner> o) =>
                o.OrderBy(banner => banner.SortOrder).ThenBy(banner => banner.CreationTime);
            var bannerPageList =
                await _bannerRepository.GetBannerPageList(expression, orderBy, input.MaxResultCount, input.SkipCount);
            var bannerDtos = ObjectMapper.Map<List<Banner>, List<BannerManagementDto>>(bannerPageList.Item1.ToList());

            var channelList = await _channelAppService.GetListAsync("");
            foreach (var item in bannerDtos)
            {
                item.ChannelName = channelList.Where(t => t.Id == item.ChannelId).Select(t => t.ChannelName).FirstOrDefault();
            }

            return new PagedResultDto<BannerManagementDto>(bannerPageList.Item2, bannerDtos);
        }

        /// <summary>
        /// 根据主键ID获取Banner信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BannerManagementDto> GetAsync(Guid id)
        {
            var bannerManagementDto = ObjectMapper.Map<Banner, BannerManagementDto>(await _bannerRepository.GetAsync(id));

            var channelList = await _channelAppService.GetListAsync("");
            bannerManagementDto.ChannelName = channelList.Where(t => t.Id == bannerManagementDto.ChannelId).Select(t => t.ChannelName).FirstOrDefault();

            return bannerManagementDto;
        }

        /// <summary>
        /// 添加一条Banner
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<BannerManagementDto> CreateAsync(CreateBannerManagementInput inputDto)
        {
            var marketingBanner = ObjectMapper.Map<CreateBannerManagementInput, Banner>(inputDto);
            var bannerManagementDto= ObjectMapper.Map<Banner, BannerManagementDto>(await _bannerRepository.InsertAsync(marketingBanner));

            var channelList = await _channelAppService.GetListAsync("");
            bannerManagementDto.ChannelName = channelList.Where(t => t.Id == bannerManagementDto.ChannelId).Select(t => t.ChannelName).FirstOrDefault();

            return bannerManagementDto;
        }


        /// <summary>
        /// 根据主键ID修改Banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<BannerManagementDto> UpdateAsync(Guid id, UpdateBannerManagementInput inputDto)
        {
            Banner banner = await _bannerRepository.GetAsync(id);
            var model = ObjectMapper.Map(inputDto, banner);
            var bannerManagementDto= ObjectMapper.Map<Banner, BannerManagementDto>(await _bannerRepository.UpdateAsync(model));

            var channelList = await _channelAppService.GetListAsync("");
            bannerManagementDto.ChannelName = channelList.Where(t => t.Id == bannerManagementDto.ChannelId).Select(t => t.ChannelName).FirstOrDefault();

            return bannerManagementDto;
        }

        /// <summary>
        /// 根据主键ID修改Banner启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<BannerManagementDto> UpdateEnableStatusAsync(Guid id, UpdateBannerManagementEnableStatusInput inputDto)
        {
            Banner banner = await _bannerRepository.GetAsync(id);
            banner.IsEnable = inputDto.IsEnable;
            var bannerManagementDto= ObjectMapper.Map<Banner, BannerManagementDto>(banner);

            var channelList = await _channelAppService.GetListAsync("");
            bannerManagementDto.ChannelName = channelList.Where(t => t.Id == bannerManagementDto.ChannelId).Select(t => t.ChannelName).FirstOrDefault();

            return bannerManagementDto;
        }

        /// <summary>
        /// 根据主键ID删除Banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _bannerRepository.DeleteAsync(id);
        }
        private Expression<Func<Banner, bool>> CommonExpression()
        {
            Expression<Func<Banner, bool>> expression = o => true;
            return expression;
        }
    }


}
