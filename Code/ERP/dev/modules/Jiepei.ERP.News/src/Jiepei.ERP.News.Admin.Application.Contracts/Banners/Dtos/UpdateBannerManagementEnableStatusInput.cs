using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.News.Admin
{
    public class UpdateBannerManagementEnableStatusInput
    {
        [Required(ErrorMessage = "启用状态不能为空")]
        public bool IsEnable { get; set; }
    }
}
