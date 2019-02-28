using System;
using Microsoft.EntityFrameworkCore;

namespace DriverTracker.Models
{
    public class MvcDriverContext : DbContext
    {
        public MvcDriverContext(DbContextOptions<MvcDriverContext> options)
            :base(options) {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Analyst> Analysts { get; set; }
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<LegCoordinates> LegCoordinates { get; set; }
    }
}
