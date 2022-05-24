using ClosedXML.Excel;
using Jiepei.ERP.StatisticalDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Jiepei.ERP.Admin.Application.Contracts.Statistics;
using Microsoft.AspNetCore.Authorization;

namespace Jiepei.ERP.Admin.Application.Statistics
{
    //[Authorize]
    public class StatisticsAppService : ERPAdminAppService
    {
        protected IRepository<StatisticalData, Guid> StatisticalDataRepository { get; }

        public StatisticsAppService(IRepository<StatisticalData, Guid> statisticalDataRepository)
        {
            StatisticalDataRepository = statisticalDataRepository;
        }

        public async Task ImportAsync(IRemoteStreamContent content)
        {
            using (var workbook = new XLWorkbook(content.GetStream()))
            {
                var worksheet = workbook.Worksheets.First();
                var rows = worksheet.RangeUsed().RowsUsed().Skip(2);
                var list = new List<StatisticalData>();

                foreach (var row in rows)
                {
                    int num;
                    double rate;
                    var test = (int)(row.Cell(9).TryGetValue(out rate) ? rate * 100 : 0);
                    if (row.Cell(1).GetString() == "汇总数据")
                    {
                        break;
                    }
                    list.Add(new StatisticalData(date: row.Cell(1).GetDateTime(),
                                                 inquiry: row.Cell(2).TryGetValue(out num) ? num : 0,
                                                 quotable: row.Cell(3).TryGetValue(out num) ? num : 0,
                                                 quoted: row.Cell(4).TryGetValue(out num) ? num : 0,
                                                 notQuoted: row.Cell(5).TryGetValue(out num) ? num : 0,
                                                 validInquiryRate: ConvertToInt((decimal)(row.Cell(6).TryGetValue(out rate) ? rate * 100 : 0)),
                                                 followUpsRate: ConvertToInt((decimal)(row.Cell(8).TryGetValue(out rate) ? rate * 100 : 0)),
                                                 followUpCustomers: row.Cell(7).TryGetValue(out num) ? num : 0,
                                                 churnCustomers: row.Cell(9).TryGetValue(out num) ? num : 0,
                                                 churnRate: ConvertToInt((decimal)(row.Cell(10).TryGetValue(out rate) ? rate * 100 : 0)),
                                                 paymentOrders: row.Cell(11).TryGetValue(out num) ? num : 0,
                                                 paymentSuccessRate: ConvertToInt((decimal)(row.Cell(12).TryGetValue(out rate) ? rate * 100 : 0)),
                                                 paymentAmount: row.Cell(13).TryGetValue(out decimal amount) ? amount : 0,
                                                 totalProductionNumber: row.Cell(14).TryGetValue(out num) ? num : 0,
                                                 ordersInProduction: row.Cell(15).TryGetValue(out num) ? num : 0,
                                                 productionRate: ConvertToInt((decimal)(row.Cell(16).TryGetValue(out rate) ? rate * 100 : 0)),
                                                 completedOrders: row.Cell(17).TryGetValue(out num) ? num : 0,
                                                 orderCompletionRate: ConvertToInt((decimal)(row.Cell(18).TryGetValue(out rate) ? rate * 100 : 0)),
                                                 totalParts: row.Cell(19).TryGetValue(out num) ? num : 0,
                                                 finishedParts: row.Cell(20).TryGetValue(out num) ? num : 0,
                                                 partCompletionRate: ConvertToInt((decimal)(row.Cell(21).TryGetValue(out rate) ? rate * 100 : 0))));
                }
                var date = GetFirstAndLastDayOfMonth();
                await StatisticalDataRepository.DeleteAsync(t => t.Date >= date.FistDay && t.Date <= date.LastDay);
                await StatisticalDataRepository.InsertManyAsync(list);
            }
        }

        [AllowAnonymous]
        public async Task<MonthDataDto> GetAnalysisAsync()
        {
            var date = GetFirstAndLastDayOfMonth();
            var list = await StatisticalDataRepository.GetListAsync(t => t.Date >= date.FistDay && t.Date <= date.LastDay);

            var result = new MonthDataDto
            {
                Inquiry = list.Select(x => x.Inquiry).Sum(),
                PaymentOrders = list.Select(x => x.PaymentOrders).Sum(),
                Quoted = list.Select(x => x.Quoted).Sum(),
                NotQuoted = list.Select(x => x.NotQuoted).Sum(),

                PaymentAmount = list.Select(x => x.PaymentAmount).Sum(),

                FollowUpCustomerRate = CalcRate(list.Select(x => x.FollowUpCustomers).Sum(), list.Select(x => x.Inquiry).Sum()),
                ChurnCustomerRate = CalcRate(list.Select(x => x.ChurnCustomers).Sum(), list.Select(x => x.Inquiry).Sum()),
                PaymentCustomerRate = CalcRate(list.Select(x => x.PaymentOrders).Sum(), list.Select(x => x.Inquiry).Sum()),

                TotalProductionNumber = list.Select(x => x.TotalProductionNumber).Sum(),
                OrdersInProduction = list.Select(x => x.OrdersInProduction).Sum(),
                TotalParts = list.Select(x => x.TotalParts).Sum(),
                FinishedParts = list.Select(x => x.FinishedParts).Sum(),

            };


            foreach (var item in list.OrderBy(t => t.Date))
            {
                result.DailyData.Add(new DailyDataDto
                {
                    Date = item.Date.ToString("M-d"),
                    Inquiry = item.Inquiry,
                    Quoted = item.Quoted,
                    PaymentOrders = item.PaymentOrders,
                    ProductionRate = item.ProductionRate,
                    OrderCompletionRate = item.OrderCompletionRate,
                    PartCompletionRate = item.PartCompletionRate
                });
            }

            return result;
        }

        public async Task<PagedResultDto<StatisticalDataDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrEmpty())
            {
                input.Sorting = nameof(StatisticalData.CreationTime);
            }
            var list = await StatisticalDataRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            var totalCount = await StatisticalDataRepository.GetCountAsync();

            return new PagedResultDto<StatisticalDataDto>(totalCount, ObjectMapper.Map<List<StatisticalData>, List<StatisticalDataDto>>(list));
        }

        protected (DateTime FistDay, DateTime LastDay) GetFirstAndLastDayOfMonth()
        {
            var date = DateTime.Now.AddYears(-1);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            return (firstDayOfMonth, lastDayOfMonth);
        }

        protected int ConvertToInt(decimal num)
        {
            return (int)Math.Round(num);
        }

        protected int CalcRate(decimal num1, decimal num2)
        {
            if (num2 > 0)
                return (int)Math.Round((num1 / num2) * 100);
            return 0;
        }
    }
}
