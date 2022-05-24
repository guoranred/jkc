using Jiepei.ERP.News.Acrcles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Artcles
{
    [RemoteService]
    [Route("api/artcle")]
    public class ArtcleController : NewsController
    {
        private readonly IArtcleAppService _artcleAppService;

        public ArtcleController(IArtcleAppService artcleAppService)
        {
            _artcleAppService = artcleAppService;
        }

        /// <summary>
        /// 获取栏目下所有分类以及部分文章主信息
        /// </summary>
        /// <param name="ColumnOwnership"></param>
        /// <returns></returns>
        [HttpGet("column")]
        public async Task<List<GetColumnTypesOutputDto>> GetColumnTypeAsync(EnumColumnOwnership columnOwnership)
        {
            return await _artcleAppService.GetColumnTypeAsync(columnOwnership);
        }


        /// <summary>
        /// 获取具体文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetOneArticleOutputDto> GetOneArticle(Guid id)
        {
            return await _artcleAppService.GetOneArticle(id);
        }


        /// <summary>
        /// 获取具体文章跟相关推荐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/andrelevant")]
        public async Task<GetOneArticleAndrelevantOutput> GetOneArticleAndrelevant(Guid id)
        {
            return await _artcleAppService.GetOneArticleAndrelevant(id);
        }

        /// <summary>
        /// 获取某种栏目的文章分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<PagedResultDto<ArticleListOutput>> GetArticlePage(GetArticlePageInput input)
        {
            return await _artcleAppService.GetArticlePage(input);
        }
    }
}
