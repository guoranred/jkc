namespace Jiepei.ERP.Suppliers.Suppliers.Dtos
{
    public class UnionfabCallbackInput
    {
        public string BeforeStatus { get; set; }
        public string AfterStatus { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public long Timestamp { get; set; }
        public string Signature { get; set; }
    }
}
