using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TimeKeeperAspMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace TimeKeeperAspMvc.Data
{
    public class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext(DbContextOptions<TimeKeeperContext> options) : base(options)
        {
        }

        public DbSet<AccessTime> AccessTimes { get; set; }

        // Rename table from 'AccessTimes' to 'AccessTime'
        // Do this via an attribute in the entity itself
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessTime>().ToTable("AccessTime");
        }
    }
}
