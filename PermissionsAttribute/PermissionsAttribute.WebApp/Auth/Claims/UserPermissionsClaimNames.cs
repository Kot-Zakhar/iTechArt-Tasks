using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Auth.Claims
{
    public static class UserPermissionsClaimNames
    {
        public static string UserPermissionsClaimTypeName = "Permissions";

        public static string GetPermissionName(Permission permission)
        {
            return Enum.GetName(typeof(Permission), Permission.GetProfileById);
        }

        public static Permission GetPermissionByName(string name)
        {
            return Enum.Parse<Permission>(name);
        }
    }
}
