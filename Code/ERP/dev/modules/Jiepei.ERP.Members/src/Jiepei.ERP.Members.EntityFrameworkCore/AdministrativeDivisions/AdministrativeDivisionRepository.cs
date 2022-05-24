using Jiepei.ERP.Members.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.AdministrativeDivisions
{
    public class AdministrativeDivisionRepository :
        EfCoreRepository<IMembersDbContext, AdministrativeDivision, Guid>, IAdministrativeDivisionRepository
    {
        public AdministrativeDivisionRepository(IDbContextProvider<IMembersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
