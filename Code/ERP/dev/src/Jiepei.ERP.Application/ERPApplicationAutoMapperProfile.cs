using AutoMapper;
using Essensoft.Paylink.WeChatPay;
using Jiepei.ERP.Utilities.Pays;

namespace Jiepei.ERP
{
    public class ERPApplicationAutoMapperProfile : Profile
    {
        public ERPApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<WeChatPayAHOption, WeChatPayOptions>();
        }
    }
}
