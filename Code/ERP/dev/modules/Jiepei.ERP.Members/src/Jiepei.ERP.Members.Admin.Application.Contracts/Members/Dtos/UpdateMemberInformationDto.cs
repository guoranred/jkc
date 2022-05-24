﻿using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members.Admin
{
    public class UpdateMemberInformationDto
    {
        /// <summary>
        /// 姓名(联系人)
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空！")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required(ErrorMessage = "手机号码不能为空！")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别选项不能为空！")]
        public byte Sex { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>

        public string QQ { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 公司类型Code(数据字典)
        /// </summary>
        public string CompanyTypeCode { get; set; }

        /// <summary>
        /// 公司类型名称(数据字典)
        /// </summary>
        public string CompanyTypeName { get; set; }

        /// <summary>
        /// 主营产品Code(数据字典)
        /// </summary>
        public string MainProductCode { get; set; }

        /// <summary>
        /// 主营产品名称(数据字典)
        /// </summary>
        public string MainProductName { get; set; }

        /// <summary>
        /// 所属行业
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// 职业属性Code(数据字典)
        /// </summary>
        public string ProfessionCode { get; set; }

        /// <summary>
        /// 职业属性名称(数据字典)
        /// </summary>
        public string ProfessionName { get; set; }
    }
}
