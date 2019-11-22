using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PermissionsAttribute.WebApp.Auth;
using PermissionsAttribute.WebApp.Auth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Filter
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
            IEnumerable<Permission> permissions = context.HttpContext.User.FindAll(UserPermissionsClaimNames.UserPermissionsClaimTypeName).Select(c => UserPermissionsClaimNames.GetPermissionByName(c.Value));
            if (!permissions.Contains(_permission))
                context.Result = new StatusCodeResult(403);
        }
    }
}
