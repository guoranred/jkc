using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members.Admin
{
    public class UpdateMemberInfomationConsultantDto
    {
        /// <summary>
        /// 客服ID
        /// </summary>
        [Required(ErrorMessage = "绑定的客服不能为空")]
        public Guid CustomerServiceId { get; set; }
    }
}
