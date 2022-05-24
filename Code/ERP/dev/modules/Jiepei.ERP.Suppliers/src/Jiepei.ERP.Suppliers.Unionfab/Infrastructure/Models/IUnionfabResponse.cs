namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models
{
    public interface IUnionfabResponse
    {
        ResponseStatus Status { get; set; }
        ErrorMessage ErrorMessage { get; set; }
    }
}
