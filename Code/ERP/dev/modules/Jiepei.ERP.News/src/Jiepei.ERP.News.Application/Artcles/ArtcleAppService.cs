using Jiepei.ERP.News.Acrcles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Artcles
{
    public class ArtcleAppService : NewsAppService, IArtcleAppService
    {
        private readonly IColumnTypeRepository _columnTypeRepository;
        private readonly IArticleListRepository _articleListRepository;

        public ArtcleAppService(IArticleListRepository articleListRepository
            , IColumnTypeRepository columnTypeRepository)
        {
            _articleListRepository = articleListRepository;
            _columnTypeRepository = columnTypeRepository;
        }


        /// <summary>
        /// 获取栏目下所有分类以及部分文章主信息
        /// </summary>
        /// <param name="ColumnOwnership"></param>
        /// <returns></returns>
        public async Task<List<GetColumnTypesOutputDto>> GetColumnTypeAsync(EnumColumnOwnership ColumnOwnership)
        {
            var columnTypeQuery = from c in _columnTypeRepository.Where(e => e.ColumnOwnership == ColumnOwnership)
                                  select new GetColumnTypesOutputDto
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      LogoImage = c.LogoImage,
                                      Alias = c.Alias
                                  };
            var columnTypes = await AsyncExecuter.ToListAsync(columnTypeQuery);

            var ids = columnTypes.Select(e => e.Id);

            var articleQuery = from a in _articleListRepository.Where(e => ids.Contains(e.ColumnTypeId) && e.ReleaseStatus == true)
                                 .OrderByDescending(e => e.IsSetTop)
                                 .ThenBy(e => e.Sort)
                                 .ThenByDescending(e => e.ReleaseTime)
                               select new ArticleListOutput
                               {
                                   Author = a.Author,
                                   Id = a.Id,
                                   ImgPath = a.ImgPath,
                                   Introduce = a.Introduce,
                                   ReleaseTime = a.ReleaseTime,
                                   Tag = a.Tag,
                                   Title = a.Title,
                                   ColumnTypeId = a.ColumnTypeId
                               };

            var articles = await AsyncExecuter.ToListAsync(articleQuery);

            foreach (var item in columnTypes)
            {
                item.ArticleLists = articles.Where(e => e.ColumnTypeId == item.Id).Take(3).ToList();
            }
            return columnTypes;
        }

        /// <summary>
        /// 获取具体文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetOneArticleOutputDto> GetOneArticle(Guid id)
        {
            var article = await _articleListRepository.FindAsync(t => t.Id == id);
            return new GetOneArticleOutputDto
            {
                Author = article.Author,
                Content = article.Content,
                ReleaseTime = article.ReleaseTime,
                Title = article.Title
            };
        }


        /// <summary>
        /// 获取具体文章跟相关推荐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetOneArticleAndrelevantOutput> GetOneArticleAndrelevant(Guid id)
        {
            //当前文章
            var curentArticleQuery = from a in _articleListRepository.Where(e => e.Id == id && e.ReleaseStatus == true)
                                     join c in _columnTypeRepository on a.ColumnTypeId equals c.Id
                                     select new { a.Content, a.Id, c.ColumnOwnership, a.Author, a.Title, a.ReleaseTime };

            var curentArticle = await AsyncExecuter.FirstAsync(curentArticleQuery);
            //相关新闻
            var otherArticleQuery = from a in _articleListRepository.Where(e => e.Id != id && e.ReleaseStatus == true)
                                    join c in _columnTypeRepository.Where(e => e.ColumnOwnership == curentArticle.ColumnOwnership) on a.ColumnTypeId equals c.Id
                                    orderby a.IsSetTop ,a.Sort, a.ReleaseTime descending
                                    select new ArticleListOutput
                                    {
                                        Author = a.Author,
                                        Id = a.Id,
                                        ImgPath = a.ImgPath,
                                        Introduce = a.Introduce,
                                        ReleaseTime = a.ReleaseTime,
                                        Tag = a.Tag,
                                        Title = a.Title,
                                        ColumnTypeId = a.ColumnTypeId
                                    };


            var otherArticle = await AsyncExecuter.ToListAsync(otherArticleQuery);

            return new GetOneArticleAndrelevantOutput
            {
                Author = curentArticle.Author,
                ReleaseTime = curentArticle.ReleaseTime,
                Title = curentArticle.Title,
                Content = curentArticle.Content,
                OtherArticles = otherArticle
            };
        }

        /// <summary>
        /// 获取某种栏目的文章分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ArticleListOutput>> GetArticlePage(GetArticlePageInput input)
        {
            var res = new PagedResultDto<ArticleListOutput>();
            var query = from c in _columnTypeRepository.Where(e => e.ColumnOwnership == input.ColumnOwnership)
                        join a in _articleListRepository.Where(e => e.ReleaseStatus == true)
                         
                         //.ThenByDescending(e => e.Sort)
                         //.ThenBy(e => e.ReleaseTime)
                         on c.Id equals a.ColumnTypeId
                        orderby a.IsSetTop descending, a.Sort descending, a.ReleaseTime descending
                        select new ArticleListOutput
                        {
                            Author = a.Author,
                            Id = a.Id,
                            ImgPath = a.ImgPath,
                            Introduce = a.Introduce,
                            ReleaseTime = a.ReleaseTime,
                            Tag = a.Tag,
                            Title = a.Title,
                            ColumnTypeId = a.ColumnTypeId
                        };
            var count = await AsyncExecuter.CountAsync(query);
            var list = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<ArticleListOutput>(count, list);
        }
    }
}
