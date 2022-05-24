using Jiepei.ERP.DeliverCentersClient.Dto;
using Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.DeliverCentersClient.DeliverCenterClients
{
    public interface ICncApi : ITransientDependency
    {
        [Put("/api/cnc/{orderNo}/cancel")]
        Task<HttpResponseMessage> CancelAsync(string orderNo, DC_CancelOrderDto input);

        [Post("/api/cnc")]
        Task<HttpResponseMessage> CreateAsync(DC_CreateDto input);

        [Put("/api/cnc/{orderNo}/pay")]
        Task<HttpResponseMessage> PaymentAsync(string orderNo, DC_PaymentDto input);

        [Put("/api/cnc/{orderNo}/confirm-receipt")]
        Task<HttpResponseMessage> ReceivingAsync(string orderNo);

        [Put("/api/cnc/{orderNo}/offline-payment")]
        Task<HttpResponseMessage> OfflinePaymentAsync(string orderNo, OfflinePaymentRequest input);
    }
}
