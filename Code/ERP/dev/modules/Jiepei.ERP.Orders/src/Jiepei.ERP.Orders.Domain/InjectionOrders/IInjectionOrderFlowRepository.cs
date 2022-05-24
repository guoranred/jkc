using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public interface IInjectionOrderFlowRepository : IRepository<InjectionOrderFlow, Guid>
    {
    }
}
