namespace Jiepei.ERP.Orders.Application.External.Cache
{
    internal class SheetMetalAccount
    {
        internal SheetMetalAccount(string userName, string password, string verificationCode = null, string uuid = null)
        {
            this.UserName = userName;
            this.Password = password;
            this.VerificationCode = verificationCode;
            this.Uuid = uuid;
        }

        public string UserName { get; internal set; }

        public string Password { get; internal set; }

        public string VerificationCode { get; internal set; }

        public string Uuid { get; internal set; }
    }
}
