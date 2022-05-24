using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ErpConsumer.Business.Contracts
{
    public interface IOrderDataService
    {
        Task<bool> OrderTaskAsync<T>(T model,string url,EnumHttpClientType clientType);
    }
}
