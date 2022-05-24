using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Materials
{
    public interface IMaterialPriceRepository : IRepository<MaterialPrice, Guid> { }
}
