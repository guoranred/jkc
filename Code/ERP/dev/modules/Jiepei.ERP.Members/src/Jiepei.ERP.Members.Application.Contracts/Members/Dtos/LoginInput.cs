using System;

namespace Jiepei.ERP.Members.Members.Dtos
{
    public class LoginInput
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public Guid ChannelId { get; set; }
    }
}
