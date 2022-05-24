using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Pays
{
    public class OrderPayLogManager : DomainService, IOrderPayLogManager
    {
        private readonly IOrderPayLogRepository _orderPayLogRepository;
        private readonly IOrderPayDetailLogRepository _orderPayDetailLogRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public OrderPayLogManager(IUnitOfWorkManager unitOfWorkManager
            , IOrderPayLogRepository orderPayLogRepository
            , IOrderPayDetailLogRepository orderPayDetailLogRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _orderPayLogRepository = orderPayLogRepository;
            _orderPayDetailLogRepository = orderPayDetailLogRepository;
        }

        public async Task CreateAsync(OrderPayLog payLog, List<OrderPayDetailLog> detailLogs)
        {
            using var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false);

            var payLogEntity = await _orderPayLogRepository.InsertAsync(payLog);
            var payLogId = payLogEntity.Id;

            var details = new List<OrderPayDetailLog>();
            foreach (var detail in detailLogs ?? new List<OrderPayDetailLog>())
            {
                detail.PayLogId = payLogId;
                details.Add(detail);
            }
            await _orderPayDetailLogRepository.InsertManyAsync(details);

            await uow.CompleteAsync();

        }

        public async Task UpdateAsync(string payCode, bool isSucess)
        {
            using var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false);

            var log = await _orderPayLogRepository.GetAsync(x => x.PayCode == payCode);
            log.IsPaySuccess = true;
            await _orderPayLogRepository.UpdateAsync(log);

            var list = new List<OrderPayDetailLog>();
            var details = await _orderPayDetailLogRepository.GetListAsync(x => x.PayLogId == log.Id);
            foreach (var item in details ?? new List<OrderPayDetailLog>())
            {
                item.IsSuccess = isSucess;
                list.Add(item);
            }
            await _orderPayDetailLogRepository.UpdateManyAsync(list);

            await uow.CompleteAsync();
        }
    }
}
