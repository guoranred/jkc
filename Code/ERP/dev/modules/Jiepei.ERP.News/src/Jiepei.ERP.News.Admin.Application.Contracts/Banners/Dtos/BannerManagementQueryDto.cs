using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin
{
    public class BannerManagementQueryDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
