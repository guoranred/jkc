using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public class InjectionOrderFlowRepository: EfCoreRepository<IOrdersDbContext,InjectionOrderFlow,Guid>, IInjectionOrderFlowRepository
    {
        public InjectionOrderFlowRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }
    }
}
