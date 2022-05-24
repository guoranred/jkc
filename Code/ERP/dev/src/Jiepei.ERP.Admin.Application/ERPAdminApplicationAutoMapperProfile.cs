using AutoMapper;
using Jiepei.ERP.Admin.Application.Contracts.Statistics;
using Jiepei.ERP.StatisticalDatas;

namespace Jiepei.ERP
{
    public class ERPAdminApplicationAutoMapperProfile : Profile
    {
        public ERPAdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<StatisticalData, StatisticalDataDto>()
                .ForMember(dest => dest.FollowUps, opt => opt.MapFrom(src => src.FollowUpsRate));
        }
    }
}
