using Jiepei.ERP.Suppliers.Unionfab.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.Suppliers.Unionfab.Services
{
    public abstract class CommonService : ITransientDependency
    {
        private ISupplierUnionfabApiRequester _supplierUnionfabApiRequester;

        public IServiceProvider ServiceProvider { get; set; }

        protected readonly object ServiceLocker = new object();
        protected TService LazyLoadService<TService>(ref TService service)
        {
            if (service == null)
            {
                lock (ServiceLocker)
                {
                    if (service == null)
                    {
                        service = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }

            return service;
        }

        protected ISupplierUnionfabApiRequester SupplierUnionfabApiRequester => LazyLoadService(ref _supplierUnionfabApiRequester);
    }
}
