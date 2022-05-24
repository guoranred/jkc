using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Orders.Materals;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Orders.Materials
{
    public class MaterialManager : DomainService, IMaterialManager
    {
        private readonly ID3MaterialRepository _d3MaterialRepository;
        private readonly IMaterialPriceRepository _materialPriceRepository;
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        public MaterialManager(ID3MaterialRepository d3MaterialsRepository
            , IMaterialPriceRepository materialPriceRepository
            , IDataDictionaryRepository dataDictionaryRepository)
        {

            _d3MaterialRepository = d3MaterialsRepository;
            _materialPriceRepository = materialPriceRepository;
            _dataDictionaryRepository = dataDictionaryRepository;
        }


        /// <summary>
        /// 3d交期日期计算
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public async Task<DateTime> Calculation3DDeliveryDays(int n)
        {
            var datetime = DateTime.Now;
            // 17:30
            var dictionaryHoliday3DTime = await _dataDictionaryRepository.GetListAsync(t => t.Code == "ThreeHolidayTime", true);
            // 20210612|20216013|20210614|20210919|20210920|20210921
            var dictionaryHoliday3DDate = await _dataDictionaryRepository.GetListAsync(t => t.Code == "ThreeHolidayDate", true);

            var dictionaryTime = dictionaryHoliday3DTime.First().Items[0].Description;
            var dictionaryDate = dictionaryHoliday3DDate.First().Items[0].Description;
            var dictionaryDatelist = dictionaryDate.Split('|');

            //是否工作时间
            var isWorkTime = IsWorkTime(dictionaryTime, datetime);
            if (isWorkTime)
                n++;

            //是否周末 节假日
            for (var i = 0; i < n + 1; i++)
            {
                var isHoliday = IsHoliday(dictionaryDatelist, datetime.AddDays(i));
                if (isHoliday)
                    n++;
            }

            return DateTime.Now.AddDays(n);
        }

        /// <summary>
        ///  3d交期天数
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="materialId"></param>
        /// <param name="handleMethod"></param>
        /// <param name="handleMethodDesc"></param>
        /// <returns></returns>
        public async Task<int> Calculation3DDelivery(Guid channelId, Guid materialId, string handleMethod, string handleMethodDesc)
        {
            var d3MaterialEntiy = await _d3MaterialRepository.GetQueryableAsync();
            var materialPriceEntiy = await _materialPriceRepository.GetQueryableAsync();
            var query = d3MaterialEntiy
                .Join(materialPriceEntiy, e => e.Id, o => o.MaterialId, (e, o) => new { e, o })
                .Where(t => t.o.MaterialId == materialId)
                .Where(t => t.o.ChannelId == channelId);
            var entiy = (await AsyncExecuter.ToListAsync(query)).First();

            var delivery = entiy.e.Delivery ?? 0;//材料基础交期 


            //后处理额外交期
            var handleMethodDelivery = GethHandleMethodDelivery(handleMethod, handleMethodDesc);

            return delivery + handleMethodDelivery;
        }

        /// <summary>
        /// 3D计价
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="materialId"></param>
        /// <param name="handleMethod"></param>
        /// <param name="num"></param>
        /// <param name="volume"></param>
        /// <param name="handleMethodDesc"></param>
        /// <returns></returns>

        public async Task<MateralValuationEto> Calculation3DPrice(Guid channelId, Guid materialId, string handleMethod, int num, decimal volume, string handleMethodDesc)
        {

            var d3MaterialEntiy = await _d3MaterialRepository.GetQueryableAsync();
            var materialPriceEntiy = await _materialPriceRepository.GetQueryableAsync();
            var query = d3MaterialEntiy
                .Join(materialPriceEntiy, e => e.Id, o => o.MaterialId, (e, o) => new { e, o })
                .Where(t => t.o.MaterialId == materialId)
                .Where(t => t.o.ChannelId == channelId);
            var entiy = (await AsyncExecuter.ToListAsync(query)).First();

            var density = Convert.ToDecimal(entiy.e.Density);//密度
            var price = entiy.o.Price;//单价
            var startPrice = entiy.o.StartPrice;//起步价
            var unitStartPrice = entiy.o.UnitStartPrice;//起步价
            var discount = entiy.o.Discount;//折扣比率

            //单价
            var unitPrice = GetUnitPrice(volume, density, price, unitStartPrice);

            //后处理费
            var handleMethodDescPrice = GetHandleMethodDescPrice(handleMethod, handleMethodDesc);

            //材料费 
            var materialPrice = (unitPrice * num) * discount;

            //销售价
            var sellingPrice = Math.Ceiling(materialPrice + handleMethodDescPrice * num);

            //比较起步价
            var resultPrice = sellingPrice < startPrice ? startPrice : sellingPrice;

            return new MateralValuationEto()
            {
                Price = resultPrice,
                UnitPrice = unitPrice
            };
        }


        /// <summary>
        /// 单价
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="density"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal GetUnitPrice(decimal volume, decimal density, decimal price, decimal unitStartPrice)
        {
            var weight = GetWeight(volume, density, 1); //重量
            var unitPrice = Math.Floor(price * weight);  // (weight < 0.5m ? 0.5m : weight);//单价
            return unitPrice > unitStartPrice ? unitPrice : unitStartPrice;
        }

        /// <summary>
        /// 重量
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="density"></param>
        /// <returns></returns>
        public decimal GetWeight(decimal volume, decimal density, int count)
        {
            return volume * density / 1000 * count;
        }


        /// <summary>
        /// 后处理费
        /// </summary>
        /// <param name="handleMethod"></param>
        /// <param name="handleMethodDesc"></param>
        /// <returns></returns>
        public int GetHandleMethodDescPrice(string handleMethod, string handleMethodDesc)
        {
            var price = 0;
            switch (handleMethod)
            {
                case "电镀":
                    price = 250;
                    break;
                case "喷漆":
                    var isMetallic = GetIsMetallic(handleMethodDesc);
                    if (isMetallic)
                        price = 250;
                    else
                        price = 200;
                    break;
                case "原色":
                    price = 0;
                    break;
                default:
                    break;
            }
            return price;

        }


        #region 私有

        /// <summary>
        /// 是否是金属色
        /// </summary>
        /// <param name="handleMethodDesc"></param>
        /// <returns></returns>
        private bool GetIsMetallic(string handleMethodDesc)
        {
            if (handleMethodDesc.Contains("金色") || handleMethodDesc.Contains("银色"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 后处理交期
        /// </summary>
        /// <param name=""></param>
        /// <param name="handleMethod"></param>
        /// <param name="handleMethodDesc"></param>
        /// <returns></returns>
        private int GethHandleMethodDelivery(string handleMethod, string handleMethodDesc)
        {
            var nDelivery = 0;
            switch (handleMethod)
            {
                case "电镀":
                    nDelivery = 4;
                    break;
                case "喷漆":
                    var nDay = GetDayHandleMethodDelivery(handleMethodDesc);
                    nDelivery = 4 + nDay;
                    break;
                case "原色":
                    nDelivery = 0;
                    break;
                default:
                    break;
            }
            return nDelivery;
        }
        /// <summary>
        /// 喷漆数量
        /// </summary>
        /// <param name="handleMethodDesc"></param>
        /// <returns></returns>
        private int GetDayHandleMethodDelivery(string handleMethodDesc)
        {
            int nDay = System.Text.RegularExpressions.Regex.Matches(handleMethodDesc, "colordata").Count - 1;
            return nDay;
        }

        /// <summary>
        /// 是否在工作时间内
        /// </summary>
        /// <param name="dictionaryTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private bool IsWorkTime(string dictionaryTime, DateTime dateTime)
        {
            var workTime = DateTime.Parse(dictionaryTime).TimeOfDay;
            var nowTime = Convert.ToDateTime(dateTime).TimeOfDay;
            if (workTime < nowTime)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断节假日 周末
        /// </summary>
        /// <param name="nDataDictionarylists"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsHoliday(string[] nDataDictionarylists, DateTime date)
        {
            //是否周末
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else
            {
                //是否节假日
                var ndate = int.Parse(date.ToString("yyyyMMdd"));
                var ndays = nDataDictionarylists.Where(t => t.Equals(date)).Count();
                if (ndays > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion
    }
}
