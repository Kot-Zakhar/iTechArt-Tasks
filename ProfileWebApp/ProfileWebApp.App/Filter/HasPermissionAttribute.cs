using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileWebApp.WebApp.Auth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileWebApp.WebApp.Filter
{
    public class HasPermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Permission _permission;
        public HasPermissionAttribute(Permission permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User
                .HasClaim(UserPermissionsClaimNames.UserPermissionsClaimTypeName, 
                    UserPermissionsClaimNames.GetPermissionName(_permission)
                )
            )
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
