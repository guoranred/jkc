using Jiepei.ERP.Orders.Channels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

using System.Linq;

namespace Jiepei.ERP.News.Admin.Application.Artcles
{
    /// <summary>
    /// 栏目
    /// </summary>
    public class ArtcleManagementAppServoice : NewsAdminAppService, IArtcleManagementAppServoice
    {
        private readonly IArticleListRepository _articleListsRepository;
        private readonly IChannelAppService _channelAppService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleListsRepository"></param>
        /// <param name="channelAppService"></param>
        public ArtcleManagementAppServoice(IArticleListRepository articleListsRepository, IChannelAppService channelAppService)
        {
            _articleListsRepository = articleListsRepository;
            _channelAppService = channelAppService;
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ActicleListDto> CreateAsync(CreateActicleListInput input)
        {
            var articleList = ObjectMapper.Map<CreateActicleListInput, ArticleList>(input);
            return ObjectMapper.Map<ArticleList, ActicleListDto>(await _articleListsRepository.InsertAsync(articleList));
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid Id)
        {
            await _articleListsRepository.DeleteAsync(Id);
        }

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ActicleListDto>> GetListAsync(ActicleListQueryInput input)
        {
            var query = await _articleListsRepository.GetQueryableAsync();

            var count = await AsyncExecuter.CountAsync(query);
            var acticleList = await AsyncExecuter.ToListAsync(query);

            List<ActicleListDto> articleListDtos = ObjectMapper.Map<List<ArticleList>, List<ActicleListDto>>(acticleList);
            var channelList = await _channelAppService.GetListAsync("");
            foreach (var item in articleListDtos)
            {
                item.ChannelName = channelList.Where(t => t.Id == Guid.Parse(item.ChannelId)).Select(t => t.ChannelName).FirstOrDefault();
            }
            return new PagedResultDto<ActicleListDto>(count, articleListDtos);
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ActicleListDto> UpdateAsync(Guid id, UpdateActicleListInput input)
        {
            ArticleList articleList = await _articleListsRepository.GetAsync(id);
            var model = ObjectMapper.Map(input, articleList);
            var acticleListDto = ObjectMapper.Map<ArticleList, ActicleListDto>(await _articleListsRepository.UpdateAsync(model));

            var channelList = await _channelAppService.GetListAsync("");
            acticleListDto.ChannelName = channelList.Where(t => t.Id == Guid.Parse(acticleListDto.ChannelId)).Select(t => t.ChannelName).FirstOrDefault();

            return acticleListDto;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ActicleListDto> GetDetailAsync(Guid Id)
        {
            var acticleInfo = (await _articleListsRepository.FindAsync(a => a.Id == Id));
            var acticleListDto = ObjectMapper.Map<ArticleList, ActicleListDto>(acticleInfo);

            var channelList = await _channelAppService.GetListAsync("");
            acticleListDto.ChannelName = channelList.Where(t => t.Id == Guid.Parse(acticleListDto.ChannelId)).Select(t => t.ChannelName).FirstOrDefault();

            return acticleListDto;
        }
    }
}
