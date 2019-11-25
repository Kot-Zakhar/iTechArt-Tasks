using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionsAttribute.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Data
{
    public class PermissionsDbContext : IdentityDbContext<IdentityProfile>
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options)
        {
        }
    }
}
