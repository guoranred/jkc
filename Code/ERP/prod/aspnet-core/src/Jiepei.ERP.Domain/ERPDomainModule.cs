using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.Abp.Data;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityProDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(SaasDomainModule),
        typeof(TextTemplateManagementDomainModule),
        typeof(LeptonThemeManagementDomainModule),
        typeof(LanguageManagementDomainModule),
        typeof(VoloAbpCommercialSuiteTemplatesModule),
        typeof(AbpEmailingModule),
        typeof(BlobStoringDatabaseDomainModule)
        )]
    [DependsOn(
        typeof(OrdersDomainModule),
        typeof(SuppliersDomainModule),
        typeof(MembersDomainModule)
        )]
    [DependsOn(typeof(AbpDataDictionaryDomainModule))]
    public class ERPDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = false;
            });

            Configure<AbpDataFilterOptions>(options =>
            {
                options.DefaultStates[typeof(IMultiTenant)] = new DataFilterState(isEnabled: false);
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "cn"));
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}
