using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin
{
    public class MemberInfomationQueryInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 业务员编号
        /// </summary>
        public Guid? SalesmanId { get; set; }

        /// <summary>
        /// 客服编号
        /// </summary>
        public Guid? CustomerServiceId { get; set; }
    }
}
