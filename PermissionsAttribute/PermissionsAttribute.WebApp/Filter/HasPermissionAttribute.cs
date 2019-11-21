using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Filter
{
    public class HasPermissionAttribute : Attribute
    {
        public HasPermissionAttribute(Permissions permissions = Permissions.None)
        {

        }
    }
}
