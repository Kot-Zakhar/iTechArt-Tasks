using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Auth.Claims
{

    public class UserPermissionClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
    {

        public UserPermissionClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(UserPermissionsClaimNames.UserPermissionsClaimTypeName, UserPermissionsClaimNames.GetPermissionName(Permission.GetProfileById)));
            return identity;
        }
    }
}
