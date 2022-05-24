using EasyAbp.Abp.DataDictionary;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(OrdersDomainSharedModule),
        typeof(AbpDataDictionaryDomainModule)
    )]
    public class OrdersDomainModule : AbpModule
    {
    }
}
