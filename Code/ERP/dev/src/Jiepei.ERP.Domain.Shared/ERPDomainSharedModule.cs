using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Localization;
using Jiepei.ERP.Members;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule)
        //typeof(AbpTenantManagementDomainSharedModule)
        )]
    [DependsOn(
        typeof(OrdersDomainSharedModule),
        typeof(SuppliersDomainSharedModule),
        typeof(AbpDataDictionaryDomainSharedModule),
        typeof(MembersDomainSharedModule),
        typeof(NewsDomainSharedModule)
        )]
    public class ERPDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ERPGlobalFeatureConfigurator.Configure();
            ERPModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ERPDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<ERPResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/ERP");

                options.DefaultResourceType = typeof(ERPResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("ERP", typeof(ERPResource));
            });
        }
    }
}
