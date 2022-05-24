using Jiepei.Module.SMS;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;
using JiePei.Abp.Sms.YiMei;

namespace Jiepei.ERP.Members
{
    [DependsOn(
        typeof(MembersDomainModule),
        typeof(MembersApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(SMSStandardModule),
        typeof(YiMeiSmsModule)
        )]
    [DependsOn(typeof(AbpSmsModule))]
    public class MembersApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MembersApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MembersApplicationModule>();
            });
        }
    }
}
