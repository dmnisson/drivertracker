using System;
using Microsoft.EntityFrameworkCore;

namespace DriverTracker.Models
{
    public class MvcDriverContext : DbContext
    {
        public MvcDriverContext(DbContextOptions<MvcDriverContext> options)
            :base(options) {
        }

        public DbSet<DriverTracker.Models.Driver> Driver { get; set; }
    }
}
