using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Materials.Dtos
{
    public class GetMaterialListDto : PagedAndSortedResultRequestDto
    {
        public string PartCode { get; set; }
    }
}
