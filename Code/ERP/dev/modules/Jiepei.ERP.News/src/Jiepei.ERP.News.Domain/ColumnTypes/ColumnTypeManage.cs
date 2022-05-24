
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.News
{
    /// <summary>
    /// 栏目类型
    /// </summary>
    public class ColumnTypeManage : DomainService, IColumnTypeManage
    {
        private IColumnTypeRepository _repository;
        public ColumnTypeManage(IColumnTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 创建栏目类型
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="pid"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <param name="type"></param>
        /// <param name="logoImage"></param>
        /// <param name="remark"></param>
        /// <param name="columnOwnership"></param>
        /// <returns></returns>
        public async Task<ColumnType> CreateColumnType(Guid channelId, Guid? pid, string code, string name, string alias, string type, string logoImage, string remark, EnumColumnOwnership columnOwnership)
        {
            Guid Id = GuidGenerator.Create();
            var columnType = new ColumnType(channelId, Id, pid, code, name, alias, type, logoImage, remark, columnOwnership);
            return await this._repository.InsertAsync(columnType);
        }

        /// <summary>
        /// 更新栏目类型
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Pid"></param>
        /// <param name="Name"></param>
        /// <param name="Alias"></param>
        /// <param name="Type"></param>
        /// <param name="LogoImage"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public async Task<ColumnType> UpdateColumnType(Guid id, Guid? pid, string name, string alias, string type, string logoImage, string remark, EnumColumnOwnership columnOwnership)
        {
            var columnType = await CheckExist(id);
            if (pid != null)
            {
                await CheckExist((Guid)pid);
            }
            columnType.SetColumnType(pid, name, alias, type, logoImage, remark, columnOwnership);
            return await this._repository.UpdateAsync(columnType);
        }

        /// <summary>
        /// 检查Id是否存在
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        internal async Task<ColumnType> CheckExist(Guid Id)
        {
            var existValue = await this._repository.FindAsync(x => x.Id == Id);
            if (existValue == null)
            {
                throw new UserFriendlyException($"栏目类型ID：{Id}不存在");
            }
            return existValue;
        }
    }
}
