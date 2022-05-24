using Jiepei.ERP.Members.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members
{
    public abstract class MembersAppService : ApplicationService
    {
        protected MembersAppService()
        {
            LocalizationResource = typeof(MembersResource);
            ObjectMapperContext = typeof(MembersApplicationModule);
        }


    }
}
