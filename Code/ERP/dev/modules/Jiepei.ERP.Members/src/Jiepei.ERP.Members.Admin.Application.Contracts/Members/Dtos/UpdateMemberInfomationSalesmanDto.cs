using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members.Admin
{
    public class UpdateMemberInfomationSalesmanDto
    {
        /// <summary>
        /// 业务员ID
        /// </summary>
        [Required(ErrorMessage = "绑定的业务员不能为空")]
        public Guid SalesmanId { get; set; }
    }
}
