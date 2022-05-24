using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin
{
    public class MemberInfomationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 客户编码 例:JKC000001
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 姓名(联系人)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public byte Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 客服名称
        /// </summary>
        public string CustomerServiceName { get; set; }

        /// <summary>
        /// 客服ID
        /// </summary>
        public Guid? CustomerServiceId { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesmanName { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public Guid? SalesmanId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 业务员推广码(选填)
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// 注册来源
        /// </summary>
        public string RegistrationSource { get; set; }

        public  Guid ChannelId { get; set; }
        public  string ChannelName { get; set; }
    }
}
