using Jiepei.ERP.Pays.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Pays
{
    public interface IPayNotifyService : IApplicationService
    {
        Task AHWeChatPayNotify();

        Task<string> AHAliPayNotify(AliPayNotify notify);
    }
}
