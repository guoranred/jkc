using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jiepei.ERP.News.Banners
{
    public class BannerAppService : NewsAppService, IBannerAppService
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerAppService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        /// <summary>
        /// 获取营销信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetBannerInfoOutputDto>> GetAsync()
        {
            var now = DateTime.Now;
            var bannerList = await _bannerRepository.GetListAsync(t => t.IsEnable == true && t.StartDate < now && t.EndDate > now);
            var res = (from b in bannerList
                       orderby b.SortOrder
                       select new GetBannerInfoOutputDto
                       {
                           Id = b.Id,
                           ImageUrl = b.ImageUrl,
                           RedirectUrl = b.RedirectUrl,
                           Title = b.Title,
                           Remark = b.Remark
                       }).ToList();
            return res;
        }
    }
}
