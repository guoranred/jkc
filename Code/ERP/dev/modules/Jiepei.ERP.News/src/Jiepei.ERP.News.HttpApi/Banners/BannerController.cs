using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.News
{
    [RemoteService]
    [Route("api/banner")]
    public class BannerController : NewsController
    {

        private readonly IBannerAppService _bannerAppService;

        public BannerController(IBannerAppService bannerAppService)
        {
            _bannerAppService = bannerAppService;
        }
        /// <summary>
        /// 获取营销信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<GetBannerInfoOutputDto>> GetAsync()
        {
            return await _bannerAppService.GetAsync();
        }

    }
}
