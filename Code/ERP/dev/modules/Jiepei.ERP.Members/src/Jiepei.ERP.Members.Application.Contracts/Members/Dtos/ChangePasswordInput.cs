using System;
using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members
{
    public class ChangePasswordInput
    {

        public Guid ChannelId { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "原密码不能为空")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "请输入6到25位数有效的原密码")]
        public string OldPassword { get; set; }


        /// <summary>
        /// 新密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "新密码不能为空")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "请输入6到25位数有效的新密码")]
        public string NewPassword { get; set; }


        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("NewPassword", ErrorMessage = "确认密码必须跟新密码保持一致")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "确认密码不能为空")]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = "请输入六位数有效的新密码")]
        public string ConfirmPassword { get; set; }
    }
}
