namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models
{
    public class UnionfabCommonResponse<T> : UnionfabCommonResponseBase
    {
        public T Data { get; set; }
    }
}
