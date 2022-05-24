using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.News
{
    public interface IColumnTypeManage : IDomainService
    {
        Task<ColumnType> CreateColumnType(Guid channelId, Guid? pid, string code, string name, string alias, string type, string logoImage, string remark, EnumColumnOwnership columnOwnership);

        Task<ColumnType> UpdateColumnType(Guid id, Guid? pid, string name, string alias, string type, string logoImage, string remark, EnumColumnOwnership columnOwnership);

    }
}
