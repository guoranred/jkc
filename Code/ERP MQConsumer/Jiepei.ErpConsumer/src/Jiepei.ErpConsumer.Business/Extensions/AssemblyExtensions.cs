using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Jiepei.ErpConsumer.Business.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesByImplementInterface<IInterface>(this Assembly self)
        {
            var interfaceType = typeof(IInterface);
            return self.GetTypes().Where(t => interfaceType.IsAssignableFrom(t) && !t.IsAbstract);
        }

        /// <summary>
        /// 获取依赖的程序集 DotNetCore使用
        /// </summary>
        /// <param name="this"></param>
        /// <param name="assemblyNameStartWith"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static List<Assembly> GetLazyReferencedAssemblies(this Assembly @this, string assemblyNameStartWith = "Jiepei.", int layer = 2)
        {
            if (@this == null)
                return new List<Assembly>();

            var assemblies = new List<Assembly>();
            var layerAssemblies = new List<Assembly>() { @this };
            var inLayer = 0;
            do
            {
                var curentLayerAssemblies = new List<Assembly>();
                foreach (var layerAssembly in layerAssemblies)
                {
                    try
                    {
                        var assemblyNames = layerAssembly.GetReferencedAssemblies().ToList();
                        assemblyNames.ForEach(assemblyName =>
                        {
                            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
                            if (!assemblies.Any(ass => ass.FullName == assembly.FullName) && (string.IsNullOrWhiteSpace(assemblyNameStartWith) || assembly.FullName.StartsWith(assemblyNameStartWith)))
                                assemblies.Add(assembly);

                            curentLayerAssemblies.Add(assembly);
                        });
                    }
                    catch { }
                }
                layerAssemblies = curentLayerAssemblies;

                inLayer++;
            } while (inLayer < layer);
            return assemblies;
        }
    }
}
