using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Filter
{
    public enum Permissions
    {
        None,
        GetProfileById,
        AddProfile,
        UpdateProfile,
        DeleteProfile,
        All = 100
    }
}
