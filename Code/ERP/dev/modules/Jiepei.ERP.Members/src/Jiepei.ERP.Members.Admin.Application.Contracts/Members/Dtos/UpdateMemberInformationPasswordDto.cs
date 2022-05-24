using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Members.Admin
{
    public class UpdateMemberInformationPasswordDto
    {
        /// <summary>
        /// 32位加密md5字符串
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
