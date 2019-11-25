using PermissionsAttribute.WebApp.Filter;
using System;

namespace PermissionsAttribute.WebApp.Auth.Claims
{
    public static class UserPermissionsClaimNames
    {
        public static string UserPermissionsClaimTypeName = "Permission";

        public static string GetPermissionName(Permission permission)
        {
            return Enum.GetName(typeof(Permission), permission);
        }

        public static Permission GetPermissionByName(string name)
        {
            return Enum.Parse<Permission>(name);
        }
    }
}
