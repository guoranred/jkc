using Jiepei.ERP.Shared.Enums.Customers;
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin
{
    public class CustomerServiceDto : CreationAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 客服名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客服手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 客服头像图片地址
        /// </summary>
        public string AvatarImage { get; set; }

        /// <summary>
        /// 客服微信名片图片地址
        /// </summary>
        public string WeChatImage { get; set; }

        /// <summary>
        /// 客服QQ号
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 客服邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 客服类型
        /// <para>0：客服</para>
        /// <para>1：业务员</para>
        /// <para>2：渠道</para>
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 客服类型
        /// <para>0：客服</para>
        /// <para>1：业务员</para>
        /// <para>2：渠道</para>
        /// </summary>
        public EnumCustomerServiceType Type { get; set; }

        /// <summary>
        /// 业务员推广码(选填)
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 业务线
        /// </summary>
        public string BusinessLine { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string JobNumber { get; set; }
    }
}
