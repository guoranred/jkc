using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Members.AdministrativeDivisions
{
    public interface IAdministrativeDivisionRepository : IRepository<AdministrativeDivision, Guid>
    {
    }
}
