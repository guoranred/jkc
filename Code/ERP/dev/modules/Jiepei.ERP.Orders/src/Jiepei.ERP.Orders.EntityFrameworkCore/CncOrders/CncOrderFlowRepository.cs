using Jiepei.ERP.Orders.CncOrders;
using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.CncOrders
{
    public class CncOrderFlowRepository : EfCoreRepository<IOrdersDbContext, CncOrderFlow, Guid>, ICncOrderFlowRepository
    {
        public CncOrderFlowRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}