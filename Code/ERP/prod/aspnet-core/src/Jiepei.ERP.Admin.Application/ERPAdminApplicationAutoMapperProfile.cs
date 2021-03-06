using AutoMapper;
using Jiepei.ERP.Users;
using Volo.Abp.AutoMapper;

namespace Jiepei.ERP
{
    public class ERPAdminApplicationAutoMapperProfile : Profile
    {
        public ERPAdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<AppUser, AppUserDto>().Ignore(x => x.ExtraProperties);
        }
    }
}