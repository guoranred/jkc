using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News
{
    public class GetArticlePageInput : PagedAndSortedResultRequestDto
    {
        public EnumColumnOwnership ColumnOwnership { get; set; }

    }
}
