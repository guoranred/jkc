using Jiepei.ERP.Members.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members
{
    public class UpdateMemberInfoInput
    {
        /// <summary>
        /// 姓名(联系人)
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "联系人不能为空")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>

        public string QQ { get; set; }

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
        /// 头像
        /// </summary>
        public IFormFile ProfilePhoto { get; set; }


        public Guid ChannelId { get; set; }

    }
}
