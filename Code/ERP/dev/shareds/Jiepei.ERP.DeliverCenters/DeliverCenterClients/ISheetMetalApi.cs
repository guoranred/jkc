using Refit;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal;
using System.Net.Http;
using Jiepei.ERP.DeliverCentersClient.Dto;

namespace Jiepei.ERP.DeliverCentersClient.DeliverCenterClients
{
    public interface ISheetMetalApi : ITransientDependency, IBaseApi
    { 
        [Put("/api/sheet-metal/{orderNo}/cancel")]
        Task<HttpResponseMessage> CancelAsync(string orderNo, DC_CancelOrderDto input);

        [Post("/api/sheet-metal")]
        Task<HttpResponseMessage> CreateAsync(DC_CreateDto input);

        [Put("/api/sheet-metal/{orderNo}/pay")]
        Task<HttpResponseMessage> PaymentAsync(string orderNo, DC_PaymentDto input);

        [Put("/api/sheet-metal/{orderNo}/confirm-receipt")]
        Task<HttpResponseMessage> ReceivingAsync(string orderNo);

        [Put("/api/sheet-metal/{orderNo}/offline-payment")]
        Task<HttpResponseMessage> OfflinePaymentAsync(string orderNo, OfflinePaymentRequest input);
    }
}
