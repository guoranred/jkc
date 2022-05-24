using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Files
{
    public class GetFileRequest : UnionfabCommonRequest
    {
        public string FileId { get; protected set; }
        public string Code { get; protected set; }

        protected GetFileRequest() { }

        public GetFileRequest(string fileId, string code)
        {
            FileId = fileId;
            Code = code;
        }
    }
}
