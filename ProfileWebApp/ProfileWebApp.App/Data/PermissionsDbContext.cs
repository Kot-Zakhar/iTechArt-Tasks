using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProfileWebApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileWebApp.WebApp.Data
{
    public class PermissionsDbContext : IdentityDbContext<IdentityProfile>
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options)
        {
        }
    }
}
