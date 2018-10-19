using System;
using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;

namespace DriverTracker.Models
{
    public class MvcDriverContext : DbContext
    {
        public MvcDriverContext(DbContextOptions<MvcDriverContext> options)
            :base(options) {
        }

        public DbSet<DriverTracker.Models.Driver> Drivers { get; set; }
        public DbSet<DriverTracker.Models.Analyst> Analysts { get; set; }
        public DbSet<DriverTracker.Models.Analysis> Analyses { get; set; }
        public DbSet<DriverTracker.Models.Leg> Legs { get; set; }
    }
}
