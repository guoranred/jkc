using JetBrains.Annotations;
using Jiepei.ERP.Members.Enums;
using System;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Members
{
    public class CustomerService : AuditedAggregateRoot<Guid>
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
        /// 客服类型  0客服  1  业务员 2 渠道业务员
        /// </summary>
        public CustomerServiceTypeEnum Type { get; set; }

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
        /// <summary>
        /// 客户信息
        /// </summary>
        public virtual Collection<MemberInformation> Members { get; protected set; }

        protected CustomerService()
        {

        }



        public CustomerService(Guid id,
                               [NotNull] string name,
                               [NotNull] string phone,
                               string avatarImage,
                               string weChatImage,
                               string qq,
                               string email,
                               CustomerServiceTypeEnum type,
                               string promoCode,
                               string businessLine,
                               bool isOnline,
                               string jobNumber) : base(id)
        {
            Name = name;
            Phone = phone;
            AvatarImage = avatarImage;
            WeChatImage = weChatImage;
            QQ = qq;
            Email = email;
            Type = type;
            PromoCode = promoCode;
            IsOnline = isOnline;
            BusinessLine = businessLine;
            JobNumber = jobNumber;
        }
    }
}
