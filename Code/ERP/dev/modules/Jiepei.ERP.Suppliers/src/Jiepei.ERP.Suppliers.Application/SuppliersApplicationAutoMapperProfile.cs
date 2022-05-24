using AutoMapper;
using Jiepei.ERP.Suppliers.Suppliers;
using Jiepei.ERP.Suppliers.Suppliers.Dtos;

namespace Jiepei.ERP.Suppliers
{
    public class SuppliersApplicationAutoMapperProfile : Profile
    {
        public SuppliersApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Supplier, SupplierDto>();
        }
    }
}