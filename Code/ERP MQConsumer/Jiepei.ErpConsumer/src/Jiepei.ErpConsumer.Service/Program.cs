using Jiepei.ErpConsumer.Business;
using Jiepei.ErpConsumer.Business.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;

namespace Jiepei.ErpConsumer.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/logs.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))

                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            //var host = new HostBuilder()
            //    .UseConsoleLifetime()
            //    .ConfigureAppConfiguration((context, configuration) =>
            //    {
            //        configuration
            //       .AddJsonFile("appsettings.json", optional: true)
            //       .AddEnvironmentVariables();
            //    })
            //    .ConfigureServices(ConfigureServices)
            //    .Build();

            //AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            //{
            //    //var logFactory = host.Services.GetService<ILoggerFactory>();
            //    //var logger = logFactory.CreateLogger<Program>();
            //    //logger.LogError(e.ExceptionObject as Exception, $"UnhandledException");
            //    Log.Error(e.ExceptionObject as Exception, "UnhandledException");
            //};
            try
            {
                Log.Information("Strating ...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
            .CreateDefaultBuilder()
            .UseWindowsService()
            //.UseConsoleLifetime()
            .UseSerilog()
            .ConfigureServices(ConfigureServices);
        /// <summary>
        /// 通用DI注入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(Program));
            var assemblies = assembly.GetLazyReferencedAssemblies("Jiepei.ErpConsumer", 2);
            assemblies.Add(assembly);
            services.AddBusinesses(assemblies.ToArray());

            services.AddHttpClient(ErpConsumerConsts.ErpClient,
                client => client.BaseAddress = new Uri(context.Configuration["Erp:BaseUrl"]))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

            ///services.AddSingleton<IHostedService, HostedService>();
            services.AddHostedService<HostedService>();
        }
    }
}
