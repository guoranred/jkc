using System;
using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Suppliers;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainModule),
        typeof(ERPAdminApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpAuditLoggingApplicationModule),
        typeof(AbpIdentityServerApplicationModule),
        typeof(AbpAccountPublicApplicationModule),
        typeof(AbpAccountAdminApplicationModule),
        typeof(LanguageManagementApplicationModule),
        typeof(LeptonThemeManagementApplicationModule),
        typeof(TextTemplateManagementApplicationModule)
        )]
    [DependsOn(
        typeof(OrdersAdminApplicationModule),
        typeof(MembersAdminApplicationModule),
        typeof(SuppliersApplicationModule)
        )]
    [DependsOn(typeof(AbpDataDictionaryApplicationModule))]
    [DependsOn(typeof(AbpEventBusRabbitMqModule))]
    public class ERPAdminApplicationModule : AbpModule
    {
        private static object typrof(object suppliersAdminApplicatinModule)
        {
            throw new NotImplementedException();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ERPAdminApplicationModule>();
            });
        }
    }
}
