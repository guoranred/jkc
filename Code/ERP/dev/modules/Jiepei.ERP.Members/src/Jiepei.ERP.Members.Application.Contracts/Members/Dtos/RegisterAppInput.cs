using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jiepei.ERP.Members.Members.Dtos
{
    public class RegisterAppInput
    {
        public Guid ChannelId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "姓名不能为空")]
        //[MinLength(2, ErrorMessage = "请输入有效的姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 电话号码请输入有效的姓名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号码不能为空")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "请输入有效的手机号")]
        public string PhoneNumber { get; set; }

        ///// <summary>
        ///// 密码
        ///// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        //[StringLength(25, MinimumLength = 6, ErrorMessage = "请输入6到26位数有效密码")]
        //public string Password { get; set; }

        ///// <summary>
        ///// 确认密码
        ///// </summary>
        //[Compare("Password", ErrorMessage = "确认密码必须跟密码保持一致")]
        //public string ConfirmPassword { get; set; }

        /// <summary>
        /// 业务员推广码(选填)
        /// </summary>
        public virtual string PromoCode { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
    }
}
