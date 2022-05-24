using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderDeliveryRepository : EfCoreRepository<IOrdersDbContext, OrderDelivery, Guid>, IOrderDeliveryRepository
    {
        public OrderDeliveryRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
