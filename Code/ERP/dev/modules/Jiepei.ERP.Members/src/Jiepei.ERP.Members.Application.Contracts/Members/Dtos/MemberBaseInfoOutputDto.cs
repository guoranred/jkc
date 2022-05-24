using Jiepei.ERP.Members.Enums;
using System;

namespace Jiepei.ERP.Members
{
    public class MemberBaseInfoOutputDto
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
        /// QQ号
        /// </summary>

        public string QQ { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 省code(数据字典)
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省名称(数据字典)
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市code(数据字典)
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 市名称(数据字典)
        /// </summary>
        public string CityName { get; set; }


        /// <summary>
        /// 头像地址
        /// </summary>
        public string ProfilePhotoUrl { get; set; }

        /// <summary>
        /// 专属客服 Id
        /// </summary>
        public virtual Guid CustomerServiceId { get; set; }
        /// <summary>
        /// 关联业务员
        /// </summary>
        public virtual Guid? SalesmanId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        // public   string CompanyName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        //public   string Email { get; set; }


        /// <summary>
        /// 业务员推广码(选填)
        /// </summary>
        // public   string PromoCode { get; set; }

        /// <summary>
        /// 公司类型Code(数据字典)
        /// </summary>
        //  public   string CompanyTypeCode { get; set; }

        /// <summary>
        /// 公司类型名称(数据字典)
        /// </summary>
        //  public   string CompanyTypeName { get; set; }

        /// <summary>
        /// 主营产品Code(数据字典)
        /// </summary>
        // public   string MainProductCode { get; set; }

        /// <summary>
        /// 主营产品名称(数据字典)
        /// </summary>
        // public   string MainProductName { get; set; }

        /// <summary>
        /// 所属行业
        /// </summary>
        //  public   string Industry { get; set; }

        /// <summary>
        /// 职业属性Code(数据字典)
        /// </summary>
        // public   string ProfessionCode { get; set; }

        /// <summary>
        /// 职业属性名称(数据字典)
        /// </summary>
        //  public   string ProfessionName { get; set; }


    }
}
