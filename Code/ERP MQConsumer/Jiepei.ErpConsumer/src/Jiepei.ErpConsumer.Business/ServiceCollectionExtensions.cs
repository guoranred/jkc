using Jiepei.ErpConsumer.Business.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Jiepei.ErpConsumer.Business
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinesses(this IServiceCollection services, Assembly[] assemblies)
        {

            foreach (var assembly in assemblies)
            {
                //singleton inject
                var singleTypes = assembly.GetTypesByImplementInterface<ISingletonInject>();
                foreach (var singleType in singleTypes)
                {
                    services.AddSingleton(singleType);
                }

                // scope inject
                var scopeTypes = assembly.GetTypesByImplementInterface<IScopeInject>();
                foreach (var scopeType in scopeTypes)
                {
                    services.AddScoped(scopeType);
                }

                // transient inject
                var transientTypes = assembly.GetTypesByImplementInterface<ITransientInject>();
                foreach (var transientType in transientTypes)
                {
                    services.AddTransient(transientType);
                }
            }

            AddApplicationDI(services);
        }

        private static void AddApplicationDI(IServiceCollection services)
        {           
            var assmbly = Assembly.GetAssembly(typeof(ServiceCollectionExtensions));
            var types = assmbly.GetTypes();
            var interfaceTypes = types.Where(oo => oo.IsInterface).Where(oo => oo.Name.EndsWith("Service"));
            foreach (var interfaceType in interfaceTypes)
            {
                var implType = types.FirstOrDefault(oo => oo.Name == interfaceType.Name.Substring(1));
                if (implType != null)
                    services.AddTransient(interfaceType, implType);
            }
        }
    }
}
