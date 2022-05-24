using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Volo.Abp.Modularity;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainSharedModule)
    )]
    [DependsOn(
        typeof(OrdersApplicationContractsModule),
        typeof(SuppliersApplicationContractsModule),
        typeof(MembersApplicationContractsModule)
        )]
    [DependsOn(typeof(AbpDataDictionaryApplicationContractsModule))]
    public class ERPApplicationContractsModule : AbpModule
    {

    }
}
