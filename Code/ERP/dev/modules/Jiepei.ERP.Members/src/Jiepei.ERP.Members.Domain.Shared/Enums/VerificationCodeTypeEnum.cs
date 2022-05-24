using System.ComponentModel;

namespace Jiepei.ERP.Members.Enums
{
    /// <summary>
    /// 验证码类型枚举
    /// </summary>
    public enum VerificationCodeTypeEnum
    {

        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register,

        /// <summary>
        /// 找回密码
        /// </summary>
        [Description("找回密码")]
        FindPassword,
    }
}
