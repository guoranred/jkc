using Jiepei.ERP.Members.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Members
{
    /// <summary>
    /// 会员基础信息
    /// </summary>
    public class MemberInformation : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 客户编码 例:JKC000001
        /// </summary>
        public virtual string Code { get; set; }

        public virtual Guid ChannelId { get; set; }

        /// <summary>
        /// 姓名(联系人)
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual GenderEnum Gender { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        public virtual string QQ { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 业务员推广码(选填)
        /// </summary>
        public virtual string PromoCode { get; set; }

        /// <summary>
        /// 公司类型Code(数据字典)
        /// </summary>
        public virtual string CompanyTypeCode { get; set; }

        /// <summary>
        /// 公司类型名称(数据字典)
        /// </summary>
        public virtual string CompanyTypeName { get; set; }

        /// <summary>
        /// 主营产品Code(数据字典)
        /// </summary>
        public virtual string MainProductCode { get; set; }

        /// <summary>
        /// 主营产品名称(数据字典)
        /// </summary>
        public virtual string MainProductName { get; set; }

        /// <summary>
        /// 所属行业
        /// </summary>
        public virtual string Industry { get; set; }

        /// <summary>
        /// 职业属性Code(数据字典)
        /// </summary>
        public virtual string ProfessionCode { get; set; }

        /// <summary>
        /// 职业属性名称(数据字典)
        /// </summary>
        public virtual string ProfessionName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string ProfilePhotoUrl { get; set; }

        /// <summary>
        /// 省code(数据字典)
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省名称(数据字典)
        /// </summary>
        public virtual string ProvinceName { get; set; }

        /// <summary>
        /// 市code(数据字典)
        /// </summary>
        public virtual string CityCode { get; set; }

        /// <summary>
        /// 市名称(数据字典)
        /// </summary>
        public virtual string CityName { get; set; }

        /// <summary>
        /// 专属客服 Id
        /// </summary>
        public virtual Guid CustomerServiceId { get; set; }
        /// <summary>
        /// 关联业务员
        /// </summary>
        public virtual Guid? SalesmanId { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 注册来源
        /// </summary>
        public string RegistrationSource { get; set; }


        public virtual CustomerService CustomerService { get; set; }

        protected MemberInformation()
        {

        }
        public MemberInformation(Guid id,
                                 Guid channelId,
                                 Guid customerServiceId,
                                 string phoneNumber,
                                 string password,
                                 string code,
                                 string name,
                                 GenderEnum gender,
                                 string promoCode,
                                 string source,
                                 string registrationSource) : base(id)
        {
            ChannelId = channelId;
            CustomerServiceId = customerServiceId;
            PhoneNumber = phoneNumber;
            Password = password;
            Code = code;
            Name = name;
            Gender = gender;
            PromoCode = promoCode;
            Source = source;
            RegistrationSource = registrationSource;
        }

        public void SetCode(string code)
        {
            Code = code;
        }
    }
}
