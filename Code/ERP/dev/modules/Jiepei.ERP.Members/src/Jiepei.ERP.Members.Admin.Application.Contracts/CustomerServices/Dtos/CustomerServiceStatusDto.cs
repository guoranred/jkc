using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin
{
    public class CustomerServiceStatusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 客服名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客服头像图片地址
        /// </summary>
        public string AvatarImage { get; set; }

        /// <summary>
        /// 服务的会员数量
        /// </summary>
        public long MemberCount { get; set; }
        /// <summary>
        /// 业务线
        /// </summary>
        public string BusinessLine { get; set; }
    }
}
