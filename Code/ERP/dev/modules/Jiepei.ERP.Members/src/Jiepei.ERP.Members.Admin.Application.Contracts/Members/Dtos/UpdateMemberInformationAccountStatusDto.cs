using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members.Admin
{
    public class UpdateMemberInformationAccountStatusDto
    {
        [Required(ErrorMessage = "账号状态不能为空")]
        public bool Status { get; set; }
    }
}
