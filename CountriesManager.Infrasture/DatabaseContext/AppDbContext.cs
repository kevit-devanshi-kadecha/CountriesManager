using CountriesManager.Core.Entities;
using CountriesManager.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CountriesManager.Infrasture.DatabaseContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Country>().HasKey(c => c.CountryId);
            builder.Entity<Country>().HasData(
            new Country() { CountryId = Guid.Parse("BF160CFD-E693-4C6A-9417-037B4501EC9B"), CountryName = "New York" });

            builder.Entity<Country>().HasData(
             new Country() { CountryId = Guid.Parse("858462EF-5587-48D5-8DB3-392938699F42"), CountryName = "London" });
        }
    }
}
