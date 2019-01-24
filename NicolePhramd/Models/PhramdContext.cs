using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicolePhramd.Models
{
    public class PhramdContext : DbContext
    {
        public PhramdContext(DbContextOptions<PhramdContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<WeatherDB> Weather { get; set; }
        public DbSet<NewsDB> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsDB>().ToTable("News");
            modelBuilder.Entity<WeatherDB>().ToTable("Weather");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
