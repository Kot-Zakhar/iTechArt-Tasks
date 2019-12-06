using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProfileWebApp.WebApp.Filter;
using ProfileWebApp.WebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfileWebApp.WebApp.Auth.Claims
{

    public class UserPermissionClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityProfile>
    {
        private UserManager<IdentityProfile> _userManager;

        public UserPermissionClaimsPrincipalFactory(
            UserManager<IdentityProfile> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityProfile user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(UserPermissionsClaimNames.UserPermissionsClaimTypeName, UserPermissionsClaimNames.GetPermissionName(Permission.GetProfiles)));
            identity.AddClaims(await _userManager.GetClaimsAsync(user));
            identity.AddClaim(new Claim("Name", user.FirstName + " " + user.LastName));
            return identity;
        }
    }
}
