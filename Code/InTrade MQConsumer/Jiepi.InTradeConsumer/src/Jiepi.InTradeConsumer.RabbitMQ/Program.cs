using Jiepi.InTradeConsumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;
namespace Jiepi.InTrade.Service
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
#else
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)              
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/logs.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            try
            {
                Log.Information("Starting console host.");
                await CreateHostBuilder(args).Build().RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseAutofac()
                .UseSerilog()
                .ConfigureAppConfiguration((context, config) =>
                {
                    //setup your additional configuration sources
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication<InTradeRabbitMQConsumerModule>();
                });
    }
}