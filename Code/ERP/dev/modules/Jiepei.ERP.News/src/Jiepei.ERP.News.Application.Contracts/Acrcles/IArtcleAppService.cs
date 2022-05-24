using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News.Acrcles
{
    public interface IArtcleAppService : IApplicationService
    {

        /// <summary>
        /// 获取栏目下所有分类以及部分文章主信息
        /// </summary>
        /// <param name="ColumnOwnership"></param>
        /// <returns></returns>
        Task<List<GetColumnTypesOutputDto>> GetColumnTypeAsync(EnumColumnOwnership ColumnOwnership);


        /// <summary>
        /// 获取具体文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetOneArticleOutputDto> GetOneArticle(Guid id);


        /// <summary>
        /// 获取具体文章跟相关推荐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetOneArticleAndrelevantOutput> GetOneArticleAndrelevant(Guid id);

        /// <summary>
        /// 获取某种栏目的文章分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleListOutput>> GetArticlePage(GetArticlePageInput input);
    }
}
