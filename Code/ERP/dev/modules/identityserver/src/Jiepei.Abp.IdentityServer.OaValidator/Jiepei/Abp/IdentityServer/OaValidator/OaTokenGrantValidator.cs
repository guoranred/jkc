using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Security.Claims;
using IdentityResource = Volo.Abp.Identity.Localization.IdentityResource;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Jiepei.Abp.IdentityServer.OaValidator
{
    public class OaTokenGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => OaValidatorConsts.OaValidatorGrantTypeName;

        private readonly ILogger<OaTokenGrantValidator> _logger;
        private readonly IEventService _eventService;
        private readonly IIdentityUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<IdentityResource> _identityLocalizer;
        private readonly IStringLocalizer<AbpIdentityServerResource> _identityServerLocalizer;
        private readonly OaValidatorOptions _options;

        public OaTokenGrantValidator(IEventService eventService,
                                     UserManager<IdentityUser> userManager,
                                     IIdentityUserRepository userRepository,
                                     IStringLocalizer<IdentityResource> identityLocalizer,
                                     IStringLocalizer<AbpIdentityServerResource> identityServerLocalizer,
                                     ILogger<OaTokenGrantValidator> logger,
                                     IOptions<OaValidatorOptions> optionsAccessor)
        {
            _logger = logger;
            _eventService = eventService;
            _userManager = userManager;
            _userRepository = userRepository;
            _identityLocalizer = identityLocalizer;
            _identityServerLocalizer = identityServerLocalizer;
            _options = optionsAccessor.Value;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var raw = context.Request.Raw;
            var credential = raw.Get(OidcConstants.TokenRequest.GrantType);
            if (credential == null || !credential.Equals(GrantType))
            {
                _logger.LogInformation("Invalid grant type: not allowed");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, _identityServerLocalizer["InvalidGrant:GrantTypeInvalid"]);
                return;
            }

            var oaIv = raw.Get(OaValidatorConsts.OaValidatorParamName);
            var oaToken = raw.Get(OaValidatorConsts.OaValidatorTokenName);

            if (oaIv.IsNullOrWhiteSpace() || oaToken.IsNullOrWhiteSpace())
            {
                _logger.LogInformation("Invalid grant type: iv or token not found");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, _identityServerLocalizer["InvalidGrant:IvOrTokenNotFound"]);
                return;
            }

            //#if DEBUG
            //            var userName = "admin";
            //#else
            var userName = CryptographyHelper.DecryptString(oaToken, _options.Key, oaIv);
            //#endif

            var currentUser = await _userRepository.FindByNormalizedUserNameAsync(StringNormalizationExtensions.Normalize(int.TryParse(userName, out int workNo) ? workNo.ToString() : userName));
            if (currentUser == null)
            {
                _logger.LogInformation("Invalid grant type: user not register");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, _identityServerLocalizer["InvalidGrant:UserNotRegister"]);
                return;
            }

            if (await _userManager.IsLockedOutAsync(currentUser))
            {
                _logger.LogInformation("Authentication failed for username: {username}, reason: locked out", currentUser.UserName);
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, _identityLocalizer["Volo.Abp.Identity:UserLockedOut"]);
                return;
            }

            var sub = await _userManager.GetUserIdAsync(currentUser);

            var additionalClaims = new List<Claim>();
            if (currentUser.TenantId.HasValue)
            {
                additionalClaims.Add(new Claim(AbpClaimTypes.TenantId, currentUser.TenantId?.ToString()));
            }

            await _eventService.RaiseAsync(new UserLoginSuccessEvent(currentUser.UserName, "", null));
            context.Result = new GrantValidationResult(sub, OidcConstants.AuthenticationMethods.Password, additionalClaims.ToArray());

            // 登录之后需要更新安全令牌
            (await _userManager.UpdateSecurityStampAsync(currentUser)).CheckErrors();
        }
    }
}
