using Jiepei.ERP.DeliverCentersClient.DeliverCenterClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.DeliverCentersClient
{
    public class DeliverCentersClientModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services
            .AddRefitClient<ICncApi>()
            .ConfigureHttpClient(c => c.BaseAddress = DeliverCenterApi(configuration))
              .AddTransientHttpErrorPolicy(p =>
              p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

            context.Services
            .AddRefitClient<ISheetMetalApi>()
            .ConfigureHttpClient(c => c.BaseAddress = DeliverCenterApi(configuration))
              .AddTransientHttpErrorPolicy(p =>
              p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));


            base.ConfigureServices(context);
        }

        private Uri DeliverCenterApi(IConfiguration configuration)
        {
            var url = configuration["DeliverCenterApiConfiguration:Url"];
            return new Uri(url);
        }
    }
}
