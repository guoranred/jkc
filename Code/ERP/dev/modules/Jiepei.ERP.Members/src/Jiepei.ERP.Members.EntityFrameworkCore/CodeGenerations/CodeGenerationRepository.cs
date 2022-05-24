using Jiepei.ERP.CodeGenerations;
using Jiepei.ERP.Members.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.CodeGenerations
{
    public class CodeGenerationRepository : EfCoreRepository<IMembersDbContext, CodeGeneration, int>, ICodeGenerationRepository
    {
        public CodeGenerationRepository(IDbContextProvider<IMembersDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}