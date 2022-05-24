using AutoMapper;

namespace Jiepei.ERP.News.Admin
{
    public class NewsAdminApplicationAutoMapperProfile : Profile
    {
        public NewsAdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            #region Banner
            CreateMap<CreateBannerManagementInput, Banner>();
            CreateMap<BannerManagementDto, Banner>().ReverseMap();
            CreateMap<UpdateBannerManagementInput, Banner>().ReverseMap();
            #endregion

            #region ArticleList
            CreateMap<ArticleList, ActicleListDto>();
            CreateMap<UpdateActicleListInput, ArticleList>();
            CreateMap<CreateActicleListInput, ArticleList>();


            #endregion

            #region ColumnTypes
            CreateMap<ColumnType, ColumnTypeDto>();

            #endregion



        }
    }
}