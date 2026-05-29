using ECOM.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        public DbSet<DM_MenuGroup> DM_MenuGroup => Set<DM_MenuGroup>();
    }
}
