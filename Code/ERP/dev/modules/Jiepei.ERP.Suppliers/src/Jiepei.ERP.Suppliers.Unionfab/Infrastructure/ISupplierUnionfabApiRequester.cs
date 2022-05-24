using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure
{
    public interface ISupplierUnionfabApiRequester
    {
        Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IUnionfabRequest unionfabRequest = null);
    }
}
