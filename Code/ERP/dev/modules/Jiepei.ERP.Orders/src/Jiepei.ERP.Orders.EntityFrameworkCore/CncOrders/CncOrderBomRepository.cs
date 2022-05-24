using Jiepei.ERP.Orders.CncOrders;
using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.CncOrders
{
    public class CncOrderBomRepository:EfCoreRepository<IOrdersDbContext, CncOrderBom,Guid>, ICncOrderBomRepository
    {
        public CncOrderBomRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider)
            : base(dbContextProvider)
    {

    }
}
}