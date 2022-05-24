using Jiepei.ERP.CodeGenerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Members.CodeGenerations
{
    public interface ICodeGenerationRepository : IRepository<CodeGeneration, int>
    {
    }
}
