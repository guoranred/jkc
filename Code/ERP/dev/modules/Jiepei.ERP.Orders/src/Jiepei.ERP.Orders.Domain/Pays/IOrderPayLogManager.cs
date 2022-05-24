using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.Pays
{
    public interface IOrderPayLogManager : IDomainService
    {

        Task CreateAsync(OrderPayLog payLog, List<OrderPayDetailLog> detailLogs);

        Task UpdateAsync(string payCode, bool isSucess);
    }
}
