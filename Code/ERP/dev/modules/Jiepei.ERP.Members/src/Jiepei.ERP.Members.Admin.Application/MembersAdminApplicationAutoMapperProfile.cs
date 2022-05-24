using AutoMapper;
using Jiepei.ERP.Members.CustomerServices;
using Jiepei.ERP.Utilities;

namespace Jiepei.ERP.Members.Admin
{
    public class MembersAdminApplicationAutoMapperProfile : Profile
    {
        public MembersAdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<CustomerService, CustomerServiceDto>()
                .ForMember(dist => dist.TypeName, opt => opt.MapFrom(src => src.Type.GetDescriptionV2()));

            CreateMap<CustomerService, CustomerServiceStatusDto>()
                .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(source => source.Members.Count));
            CreateMap<UpdateCustomerServiceDto, CustomerService>();

            CreateMap<SalesmanWithMemberQueryResultItem, CustomerServiceStatusDto>();

            CreateMap<MemberInformation, MemberInfomationDto>();
            CreateMap<UpdateMemberInformationDto, MemberInformation>();
            CreateMap<MemberInformation, MemberInformationDetailDto>();
            CreateMap<UpdateMemberInformationPasswordDto, MemberInformation>();
        }
    }
}