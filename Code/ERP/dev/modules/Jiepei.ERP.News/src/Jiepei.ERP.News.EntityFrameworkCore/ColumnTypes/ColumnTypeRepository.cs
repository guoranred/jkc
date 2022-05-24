using Jiepei.ERP.News.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.News.ColumnTypes
{
    public class ColumnTypeRepository : EfCoreRepository<INewsDbContext, ColumnType, Guid>, IColumnTypeRepository
    {
        public ColumnTypeRepository(IDbContextProvider<INewsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}