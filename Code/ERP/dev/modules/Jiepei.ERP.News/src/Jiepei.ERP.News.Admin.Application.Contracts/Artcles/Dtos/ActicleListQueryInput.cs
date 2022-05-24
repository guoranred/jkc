using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin
{
    /// <summary>
    /// 文章列表查询Dto
    /// </summary>
    [Serializable]
    public class ActicleListQueryInput : PagedAndSortedResultRequestDto
    {
    }
}
