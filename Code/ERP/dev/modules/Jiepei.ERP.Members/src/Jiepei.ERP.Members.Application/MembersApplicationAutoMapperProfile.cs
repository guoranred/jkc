using AutoMapper;
using Jiepei.ERP.Members.CustomerServices.Dtos;
using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.CodeGenerations;

namespace Jiepei.ERP.Members
{
    public class MembersApplicationAutoMapperProfile : Profile
    {
        public MembersApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<MemberAddress, MemberAddressDto>();
            CreateMap<CreateMemberAddressInput, MemberAddress>();
            CreateMap<UpdateMemberAddressInput, MemberAddress>();
            CreateMap<AdministrativeDivision, AdministrativeDivisionDto>();
            CreateMap<MemberInformation, GetMemberDot>();
            CreateMap<MemberInformation, MemberBaseInfoOutputDto>();
            CreateMap<CustomerService, GetCurrentUserCustomerServiceOutputDto>();


            CreateMap<CustomerService, CustomerServiceDto>();
            #region CodeGenerations
            CreateMap<CodeGeneration, CodeGenerationDto>();
            #endregion

        }
    }
}