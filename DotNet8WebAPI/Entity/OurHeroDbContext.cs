// OurHeroDbContext.cs
using DotNet8WebAPI.Model;

using Microsoft.EntityFrameworkCore;

namespace DotNet8WebAPI.Entity
{
    public class OurHeroDbContext : DbContext
    {
        public OurHeroDbContext(DbContextOptions<OurHeroDbContext> options) : base(options)
        {
        }

        // Registered DB Model in OurHeroDbContext file
        public DbSet<OurHero> OurHeros { get; set; }
        public DbSet<User> Users { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting a primary key in OurHero model
            modelBuilder.Entity<OurHero>().HasKey(x => x.Id);

            // Inserting record in OurHero table
            modelBuilder.Entity<OurHero>().HasData(
                new OurHero
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "",
                    isActive = true,
                }
            );

            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 1,
                  FirstName = "System",
                  LastName = "",
                  Username = "System",
                  Password = "System",
              }
          );
        }
    }
}
