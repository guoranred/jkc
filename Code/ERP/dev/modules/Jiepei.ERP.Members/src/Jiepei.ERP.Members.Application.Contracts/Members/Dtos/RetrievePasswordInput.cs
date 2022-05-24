using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members
{
    public class RetrievePasswordInput
    {
        /// <summary>
        /// 电话号码
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号码不能为空")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "请输入有效地手机号")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "新密码不能为空")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "请输入六位数有效密码")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("Password", ErrorMessage = "确认密码必须跟新密码保持一致")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入有效地验证码")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "请输入六位数有效验证码")]
        public string ValidateCode { get; set; }


        public Guid ChannelId { get; set; }
    }
}
