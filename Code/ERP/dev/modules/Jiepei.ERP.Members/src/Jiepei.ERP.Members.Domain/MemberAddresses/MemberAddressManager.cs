using Jiepei.ERP.Members.MemberAddresses;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Members
{
    public class MemberAddressManager : DomainService, IMemberAddressManager
    {
        private readonly IMemberAddressRepository _memberAddresses;
        public MemberAddressManager(IMemberAddressRepository memberAddresses)
        {
            _memberAddresses = memberAddresses;
        }
        public async Task<MemberAddress> CreateAsync(MemberAddress input)
        {
            if (input.IsDefault)
                await RemoveDefaultAsync(input.Id, input.MemberId);
            return await _memberAddresses.InsertAsync(input);
        }

        /// <summary>
        /// 更新收货地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MemberAddress> ChangeAddress(MemberAddress input)
        {
            if (input.IsDefault)
                await RemoveDefaultAsync(input.Id, input.MemberId);
            return await _memberAddresses.UpdateAsync(input);
        }

        private async Task RemoveDefaultAsync(Guid id, Guid uid)
        {
            var addresses = await _memberAddresses.GetListAsync(t => t.Id != id && t.MemberId == uid);
            addresses.ForEach(t => t.RemoveDefault());
            await _memberAddresses.UpdateManyAsync(addresses);
        }
    }
}
