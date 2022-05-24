using Jiepei.ERP.Members.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Members.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MembersAdminController : AbpController
    {
        /// <summary>
        /// 
        /// </summary>
        protected MembersAdminController()
        {
            LocalizationResource = typeof(MembersResource);
        }
    }
}
