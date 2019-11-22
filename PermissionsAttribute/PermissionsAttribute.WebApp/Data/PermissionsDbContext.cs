using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Data
{
    public class PermissionsDbContext : IdentityDbContext
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options)
        {
        }
    }
}
