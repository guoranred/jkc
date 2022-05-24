using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.CncOrders
{
    public interface ICncOrderFlowRepository : IRepository<CncOrderFlow, Guid>
    {
    }
}
