using Jiepei.ERP.Members.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Members
{
    public abstract class MembersController : AbpController
    {
        protected MembersController()
        {
            LocalizationResource = typeof(MembersResource);
        }
    }
}
